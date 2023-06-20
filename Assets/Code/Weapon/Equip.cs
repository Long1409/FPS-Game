using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public Transform PrimaryWeapon, SecondaryWeapon;

    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private GameObject pickupUI;

    [SerializeField]
    private Transform fpsCam;

    private RaycastHit hit;

    public float pickupRange;

    private void Update()
    {   
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Hightlight>()?.ToggleHighlight(false);
            pickupUI.SetActive(false);
        }
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.forward, out hit, pickupRange, pickableLayerMask))
        {
            hit.collider.GetComponent<Hightlight>().ToggleHighlight(true);
            pickupUI.SetActive(true);
        }
    }
    /*
    private void checkWeapon()
    {
        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, pickupRange))
        {
            if (hit.transform.tag == "Weapons")
            {
                canPick = true;
            }
        }
    }

    private void PickUp()
    {
        currentWeapon = wp;
        currentWeapon.transform.position = PrimaryWeapon.position;
        currentWeapon.transform.parent = PrimaryWeapon;
        currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Drop()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon = null;
    }
    */
}
