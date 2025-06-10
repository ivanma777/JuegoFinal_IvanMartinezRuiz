using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCleaningTask", menuName = "Tasks/Cleaning Task")]
public class CleaningTaskSO : TaskSO
{
    [Header("Datos específicos de limpieza")]
    public float timeLimit = 15f;
    public int numberOfDirtSpots = 5;
    public GameObject dirtPrefab;
    
    public bool taskComplete;
}
