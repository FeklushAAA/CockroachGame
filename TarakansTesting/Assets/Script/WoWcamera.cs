
using UnityEngine;
using System.Collections;

public class WoWcamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private CameraCharacteristics _camCharacteristics;

    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;

    [SerializeField] private float xDeg = 0.0f;

    [SerializeField] private float yDeg = 0.0f;
       
    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;
        xDeg = angles.x;
        yDeg = angles.y;
        
        currentDistance = _camCharacteristics.distance;
        desiredDistance = _camCharacteristics.distance;
        correctedDistance = _camCharacteristics.distance;
        
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    void Update ()
    {
            Vector3 vTargetOffset;
           
            // Don't do anything if target is not defined
            if (!target)
                    return;
            yDeg = Mathf.Clamp(yDeg, _camCharacteristics.MinVerticalAngle, _camCharacteristics.MaxVerticalAngle); // ограничиваем угол возвышения камеры        
            xDeg += Input.GetAxis("Mouse X") * _camCharacteristics.xSpeed * 0.02f;
            yDeg -= Input.GetAxis("Mouse Y") * _camCharacteristics.ySpeed * 0.02f;
            float targetRotationAngle = target.eulerAngles.y;
            float currentRotationAngle = transform.eulerAngles.y;
           
            // set camera rotation
            Quaternion rotation = Quaternion.Euler (yDeg, xDeg, 0);
           
            // calculate the desired distance
            desiredDistance -= Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * _camCharacteristics.zoomRate * Mathf.Abs (desiredDistance);
            desiredDistance = Mathf.Clamp (desiredDistance, _camCharacteristics.minDistance, _camCharacteristics.maxDistance);
            correctedDistance = desiredDistance;
           
            // calculate desired camera position
            vTargetOffset = new Vector3 (0, -_camCharacteristics.targetHeight, 0);
            Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset);
           
            // check for collision using the true target's desired registration point as set by user using height
            RaycastHit collisionHit;
            Vector3 trueTargetPosition = new Vector3 (target.position.x, target.position.y + _camCharacteristics.targetHeight, target.position.z);
           
            // if there was a collision, correct the camera position and calculate the corrected distance
            bool isCorrected = false;
            if (Physics.Linecast (trueTargetPosition, position, out collisionHit, _camCharacteristics.collisionLayers.value))
            {
                    // calculate the distance from the original estimated position to the collision location,
                    // subtracting out a safety "offset" distance from the object we hit.  The offset will help
                    // keep the camera from being right on top of the surface we hit, which usually shows up as
                    // the surface geometry getting partially clipped by the camera's front clipping plane.
                    correctedDistance = Vector3.Distance (trueTargetPosition, collisionHit.point) - _camCharacteristics.offsetFromWall;
                    isCorrected = true;
            }
           
            // For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance
            currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp (currentDistance, correctedDistance, Time.deltaTime * _camCharacteristics.zoomDampening) : correctedDistance;
           
            // keep within legal limits
            currentDistance = Mathf.Clamp (currentDistance, _camCharacteristics.minDistance, _camCharacteristics.maxDistance);
           
            // recalculate position based on the new currentDistance
            position = target.position - (rotation * Vector3.forward * currentDistance + vTargetOffset);
           
            transform.rotation = rotation;
            transform.position = position;
    }
       
    private static float ClampAngle (float angle, float min, float max)
    {
            if (angle < -360)
                    angle += 360;
            if (angle > 360)
                    angle -= 360;
            return Mathf.Clamp (angle, min, max);
    }
}
 