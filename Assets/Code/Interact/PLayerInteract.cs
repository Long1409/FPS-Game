using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerInteract : MonoBehaviour
{
    public Transform fpscam;
    [SerializeField]
    private float interactRange;

    [SerializeField]
    private LayerMask mask;

    private void Update()
    {
        Ray ray = new Ray(fpscam.transform.position, fpscam.transform.forward);

        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, interactRange, mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null) 
            {
                Debug.Log(hitInfo.collider.GetComponent<Interactable>().promptMessage);
            }
        }
    }
}
