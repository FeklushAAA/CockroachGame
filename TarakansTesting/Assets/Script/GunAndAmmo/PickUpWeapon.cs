using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] private Camera ThirdCamera;

    [SerializeField] private GameObject weaponPlace;

    [SerializeField] private float distance = 15f;

    private GameObject currentWeapon;

    private bool canPickUp = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PickUp();
        if (Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    void PickUp()
    {
        RaycastHit hit;
        Ray ray = ThirdCamera.ScreenPointToRay(new Vector3(ThirdCamera.pixelWidth / 2, ThirdCamera.pixelHeight / 2 , 0));
        if(Physics.Raycast(ray, out hit, distance))
        {
            if(hit.transform.tag == "Weapon")
            {
                if (canPickUp) Drop();

                currentWeapon = hit.transform.gameObject;
                currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
                currentWeapon.GetComponent<Collider>().isTrigger = true;
                currentWeapon.transform.parent = transform;
                currentWeapon.transform.localPosition = Vector3.zero;
                currentWeapon.transform.localEulerAngles = new Vector3(0f, 180f, 10f);
                canPickUp = true;
            }
        }

        
    }

    void Drop()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon.GetComponent<Collider>().isTrigger = false;
        canPickUp = false;
        currentWeapon = null;
    }
}
