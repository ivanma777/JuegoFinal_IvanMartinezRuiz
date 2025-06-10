using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewInspectionTask", menuName = "Tasks/Inspection Task")]

public class InspectionSO : TaskSO
{
    [Header("Datos especificos de Inspection")]
    [SerializeField] public List<InspectableItemSO> possibleItems;

    public float timeLimit;

    public Sprite[] referenceImages;

    public int objectsIn = 5;

    
}
