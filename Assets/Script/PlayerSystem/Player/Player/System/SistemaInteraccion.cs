using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaInteraccion : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    private Camera cam;

    private Transform currentInteraction;

    public bool openDoor;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Detection();

    }


    private void Detection()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, maxDistance))
        {

            if (hitInfo.transform.TryGetComponent(out PuertaContainer scriptCaja))
            {
                currentInteraction = scriptCaja.transform;
                scriptCaja.transform.GetComponent<Outline>().enabled = true;
                CanvasManager.Instance.ShowInteraction();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(openDoor)
                    {
                        scriptCaja.CerrarPuerta();

                    }
                    else
                    {
                        scriptCaja.AbrirPuerta();

                        
                    }


                }
                return;
            }

            if (hitInfo.transform.TryGetComponent(out DirtSpot dirtSpot))
            {
                currentInteraction = dirtSpot.transform;
                dirtSpot.transform.GetComponent<Outline>().enabled = true; 

                CanvasManager.Instance.ShowInteraction();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    dirtSpot.Clean();
                }

                return;
            }
            if (hitInfo.transform.TryGetComponent(out PaperCollect paper))
            {
                currentInteraction = paper.transform;
                paper.transform.GetComponent<Outline>().enabled = true; 

                CanvasManager.Instance.ShowInteraction();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    paper.Collect();
                }

                return;
            }



        }
        else if (currentInteraction != null)
        {
            CanvasManager.Instance.HideInteraction();
            currentInteraction.GetComponent<Outline>().enabled = false;
            currentInteraction = null;
        }
    }
}
