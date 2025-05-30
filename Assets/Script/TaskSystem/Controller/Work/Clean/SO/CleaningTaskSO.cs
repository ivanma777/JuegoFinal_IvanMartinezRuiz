using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCleaningTask", menuName = "Tasks/Cleaning Task")]
public class CleaningTaskSO : ScriptableObject
{
    public float timeLimit = 15f;
    public int numberOfDirtSpots = 5;
    public GameObject dirtPrefab;
    public Vector3[] spawnPositions;
}
