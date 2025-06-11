using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private GameObject pickUpUI;
    [SerializeField][Min(1)] private float hitRange = 3f;

    private Camera cam;
    private IInteractuable interactuableActual;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, hitRange, pickableLayerMask))
        {
            if (hit.transform.TryGetComponent(out IInteractuable pickable))
            {
                interactuableActual = pickable;

                Outline outline = hit.transform.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                }
                pickUpUI?.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickable.Interact();
                }
            }
        }
        else
        {
            if (interactuableActual != null)
            {
                Outline outline = (interactuableActual as MonoBehaviour)?.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = false;
                }
                interactuableActual = null;
                pickUpUI?.SetActive(false);
            }
        }
    }
}

