using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableObject : MonoBehaviour, IInteractuable
{
    [SerializeField] InspectionTaskSystem taskSystem;

    public void Interact()
    {
        //taskSystem.RevisarObjeto(gameObject);
    }
}
