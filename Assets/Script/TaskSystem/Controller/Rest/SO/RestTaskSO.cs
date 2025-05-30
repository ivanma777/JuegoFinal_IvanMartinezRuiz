using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tasks/Rest Task")]
public class RestTaskSO : ScriptableObject
{
    public string taskName;
    public float durationHours;
    public int mentalHealthRecovery;
}
