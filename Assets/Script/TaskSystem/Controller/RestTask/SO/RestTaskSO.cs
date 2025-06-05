using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tasks/Rest Task")]
public class RestTaskSO : TaskSO
{
    [Header("Datos específicos de Descanso")]
    public string taskName;
    public float hoursToAdvance;
    public int mentalHealthRecovery;
    public bool active;
   
    public int totalPapers = 3; // o los que decidas

}
