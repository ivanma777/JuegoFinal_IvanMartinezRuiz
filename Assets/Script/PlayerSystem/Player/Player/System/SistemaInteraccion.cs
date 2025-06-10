using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaInteraccion : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    private Camera cam;

    private Transform interactuableActual;

    public bool puertaAbierta;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Deteccion();

    }

    

    // Update is called once per frame


    private void Deteccion()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, maxDistance))
        {

            if (hitInfo.transform.TryGetComponent(out PuertaContainer scriptCaja))
            {
                interactuableActual = scriptCaja.transform;
                scriptCaja.transform.GetComponent<Outline>().enabled = true;
                CanvasManager.Instance.ShowInteraction();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(puertaAbierta)
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
                interactuableActual = dirtSpot.transform;
                dirtSpot.transform.GetComponent<Outline>().enabled = true; // si tiene outline opcional

                CanvasManager.Instance.ShowInteraction();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    dirtSpot.Clean();
                }

                return;
            }
            if (hitInfo.transform.TryGetComponent(out PaperCollect paper))
            {
                interactuableActual = paper.transform;
                paper.transform.GetComponent<Outline>().enabled = true; // si tiene outline opcional

                CanvasManager.Instance.ShowInteraction();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    paper.Collect();
                }

                return;
            }



        }
        else if (interactuableActual != null)
        {
            CanvasManager.Instance.HideInteraction();
            interactuableActual.GetComponent<Outline>().enabled = false;
            interactuableActual = null;
        }
    }
}
