using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpWeapon : MonoBehaviour
{
    [Header ("Объект с камерой")]

    [Space (10)]

    [SerializeField] private Camera ThirdCamera;

    [Header ("Оружие, которое будет в начале сцены")]

    [Space (10)]

    [SerializeField] private GameObject firstWeapon;

    [Header ("Дистанция, на которой можно поднять оружие")]

    [Space (10)]

    [SerializeField] private float distance = 15f;

    [Header ("UI элемент руки и текста, которые будут появляться при наведении на оружие")]

    [Space (10)]

    [SerializeField] private GameObject crosshair;

    private GameObject currentWeapon;

    private bool canPickUp = false;

    private void Start()
    {
        currentWeapon = firstWeapon;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
        currentWeapon.GetComponent<Collider>().isTrigger = true;
        currentWeapon.transform.parent = transform;
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 10f);
        canPickUp = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PickUp();
        // if (Input.GetKeyDown(KeyCode.Q)) Drop();
        OnWeaponSee();
    }

    private void PickUp()
    {
        RaycastHit hit;
        Ray ray = ThirdCamera.ScreenPointToRay(new Vector3(ThirdCamera.pixelWidth / 2, ThirdCamera.pixelHeight / 2 , 0));
        if(Physics.Raycast(ray, out hit, distance))
        {
            if(hit.transform.tag == "Weapon")
            {
                if (canPickUp) Drop();

                crosshair.SetActive(!crosshair.activeSelf);

                currentWeapon = hit.transform.gameObject;
                currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
                currentWeapon.GetComponent<Collider>().isTrigger = true;
                currentWeapon.transform.parent = transform;
                currentWeapon.transform.localPosition = Vector3.zero;
                currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 10f);
                canPickUp = true;
            }
            else
            {
                crosshair.SetActive(!crosshair.activeSelf);
            }
        }
    }

    private void OnWeaponSee()
    {
        RaycastHit hit;
        Ray ray = ThirdCamera.ScreenPointToRay(new Vector3(ThirdCamera.pixelWidth / 2, ThirdCamera.pixelHeight / 2 , 0));
        if(Physics.Raycast(ray, out hit, distance))
        {
            if(hit.transform.tag == "Weapon")
            {
                crosshair.SetActive(true);
            }
            else
            {
                crosshair.SetActive(false);
            }
        }
    }

    private void Drop()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon.GetComponent<Collider>().isTrigger = false;
        canPickUp = false;
        currentWeapon = null;
    }
}
