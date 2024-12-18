using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    [SerializeField] private LayerMask pickableLayerMask;

    private Transform interactuableActual;

    private Camera cam;


    [SerializeField] private GameObject pickUpUI;

    [SerializeField]
    [Min(1)] private float hitRange = 3;

    private RaycastHit hit;

    // Update is called once per frame
     void Start()
    {

        cam = Camera.main;
    }

    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, hitRange))
        {
            //hit.collider.GetComponent<Outline>().enabled = (true);

            if (hit.transform.TryGetComponent(out Pickeable scriptPickeable))
            {
                pickUpUI.SetActive(true);
                interactuableActual = scriptPickeable.transform;
                scriptPickeable.transform.GetComponent<Outline>().enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    


                }
            }

        }
        else if (interactuableActual != null)
        {
            interactuableActual.GetComponent<Outline>().enabled = false;
            pickUpUI.SetActive(false);

        }

    


        

        
    }
}
