using UnityEngine;

public abstract class TaskSO : ScriptableObject
{
    [Header("Información general")]
    public string titulo;
    [TextArea] public string descripcion;
    public string objetivo;

    [Header("Resultado")]
    public string recompensaTexto;
    public string castigoTexto;
}
