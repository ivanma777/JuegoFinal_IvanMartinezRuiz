using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    [SerializeField] private LayerMask pickableLayerMask;

    private IInteractuable interactuableActual;

    private Camera cam;

    [SerializeField] GameObject tabla;



    [SerializeField] private GameObject pickUpUI;

    [SerializeField]
    [Min(1)] private float hitRange = 3;

    private RaycastHit hit;

    public void Interact()
    {
        
    }

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

            if (hit.transform.TryGetComponent(out IInteractuable pickeable))
            {
                
                //pickUpUI.SetActive(true);
                pickeable.transform.GetComponent<Outline>().enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickeable.Interact();

                }
            }

        }
        else if (interactuableActual != null)
        {
            interactuableActual.transform.GetComponent<Outline>().enabled = false;
            pickUpUI.SetActive(false);

        }

    


        

        
    }
}
