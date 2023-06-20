using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{   
    public float pickupDistance = 2f; // Distance from player at which weapons can be picked up
    public Transform handTransform; // Transform of player's hand
    public Transform fpsCam;// Maximum number of weapons the player can carry
    public bool equipped = false;

    void Update()
    {
        // Check for input to pick up or drop weapon
        if (Input.GetKeyDown(KeyCode.E))
        {
            pickUp();
        }
    }

    private void pickUp() 
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, pickupDistance))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PickUp")) // If raycast hits a weapon, pick it up
            {
                foreach (var c in hit.transform.GetComponentsInChildren<Collider>()) if (c != null) c.enabled = false;
                foreach (var r in hit.transform.GetComponentsInChildren<Rigidbody>()) if (r != null) r.isKinematic = true;
                GameObject weapon = hit.collider.gameObject;
                weapon.transform.SetParent(handTransform); // Set weapon as child of player's hand
                weapon.transform.localPosition = Vector3.zero; // Reset weapon's position to player's hand
                weapon.transform.localRotation = Quaternion.identity; // Reset weapon's rotation to player's hand
                equipped = true;
            }
        }
    }
}
