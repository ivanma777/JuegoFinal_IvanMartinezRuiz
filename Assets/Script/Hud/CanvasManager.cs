using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [Header("Referencias UI")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private TMP_Text resultText;

    [Header("Temporizador")]
    [SerializeField] private TMP_Text timerText;  // Asignar en inspector
    private float maxTime;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowTask(TaskSO task)
    {
        titleText.text = task.titulo;
        descriptionText.text = task.descripcion;
        objectiveText.text = task.objetivo;
        resultText.text = "";
        timerText.text = "";
        timerText.gameObject.SetActive(false);
    }

    public void ShowResult(bool success, TaskSO task)
    {
        resultText.text = success ? task.recompensaTexto : task.castigoTexto;
        timerText.gameObject.SetActive(false);
    }

    public void HideTask()
    {
        titleText.text = "";
        descriptionText.text = "";
        objectiveText.text = "";
        resultText.text = "";
        timerText.text = "";
        timerText.gameObject.SetActive(false);
    }

    // NUEVOS MÉTODOS PARA EL TEMPORIZADOR

    public void StartTimer(float duration)
    {
        maxTime = duration;
        timerText.gameObject.SetActive(true);
        UpdateTimer(duration);  // Mostrar tiempo inicial
    }

    public void UpdateTimer(float timeLeft)
    {
        // Mostrar el tiempo restante redondeado al entero
        timerText.text = Mathf.Ceil(timeLeft).ToString() + "s";
    }

    public void StopTimer()
    {
        timerText.gameObject.SetActive(false);
    }
}
