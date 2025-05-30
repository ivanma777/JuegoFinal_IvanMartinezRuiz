using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InspectionTask")]
public class InspectionSO : TaskSO
{

    public List<GameObject> possibleItems; 

    public Sprite[] referenceImages;

    public int objectsIn = 5;

    
}
