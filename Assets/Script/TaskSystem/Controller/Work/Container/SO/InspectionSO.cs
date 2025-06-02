using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InspectionTask")]
public class InspectionSO : TaskSO
{
    [Header("Datos específicos de Inspection")]
    public List<GameObject> possibleItems;

    public float timeLimit;

    public Sprite[] referenceImages;

    public int objectsIn = 5;

    
}
