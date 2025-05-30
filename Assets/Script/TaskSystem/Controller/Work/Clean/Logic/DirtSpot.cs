using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtSpot : MonoBehaviour
{
    private CleaningTaskSystem system;

    public void Init(CleaningTaskSystem sys) => system = sys;

    private void OnMouseDown() // O usar Trigger si prefieres.
    {
        system.OnDirtCleaned(gameObject);
    }
}
