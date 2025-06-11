using UnityEngine;

public abstract class TaskSO : ScriptableObject
{
    [Header("Informaci�n general")]
    public string title;
    [TextArea] public string description;
    public string objetive;

    [Header("Resultado")]
    public string textReward;
    public string TextFail;
}
