using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject player;

    public Vector3[] defaultPositions;

    public GameObject[] weapons;

    private bool[] isEquipped;

    private int maxWeapons = 2;

    private int numWeapons = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 4f))
            {
                GameObject hitObject = hit.collider.gameObject;
                int weaponIndex = -1;

                for (int i = 0; i < weapons.Length; i++)
                {
                    if (hitObject == weapons[i] && !isEquipped[i])
                    {
                        weaponIndex = i;
                        break;
                    }
                }

                if (weaponIndex != -1)
                {
                    if (numWeapons < maxWeapons)
                    {
                        weapons[weaponIndex].transform.parent = player.transform;

                        weapons[weaponIndex].transform.localPosition = defaultPositions[weaponIndex];

                        weapons[weaponIndex].transform.localRotation = Quaternion.identity;

                        weapons[weaponIndex].GetComponent<Collider>().enabled = false;
                        weapons[weaponIndex].GetComponent<Rigidbody>().isKinematic = true;

                        isEquipped[weaponIndex] = true;

                        numWeapons++;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (isEquipped[i])
                {
                    weapons[i].transform.parent = null;

                    weapons[i].GetComponent<Collider>().enabled = true;
                    weapons[i].GetComponent<Rigidbody>().isKinematic = false;

                    isEquipped[i] = false;

                    numWeapons--;

                    break;
                }
            }
        }
    }

    void Start()
    {
        isEquipped = new bool[weapons.Length];
        for (int i = 0; i < weapons.Length; i++)
        {
            isEquipped[i] = false;
        }
    }
}

