using UnityEngine;

public class EventSystem : MonoBehaviour
{
    [Header("Eventos")]
    [SerializeField] private CleaningTaskEvent cleaningTaskEvent;
    [SerializeField] private InspectionTaskEvent inspectionTaskEvent;
    [SerializeField] private RestTaskEvent restTaskEvent;

    [Header("Tareas")]
    [SerializeField] private CleaningTaskSO cleaningTask;       // Solo uno
    [SerializeField] private InspectionSO inspectionTask;       // Solo uno
    [SerializeField] private RestTaskSO restTask;               // Solo uno

    private bool isTaskActive = false;

    private void OnEnable()
    {
        EventTimeLine.TimeToTriggerEvent += OnTimeToTriggerEvent;
    }

    private void OnDisable()
    {
        EventTimeLine.TimeToTriggerEvent -= OnTimeToTriggerEvent;
    }

    private void OnTimeToTriggerEvent()
    {
        if (isTaskActive) return; // No lanzar nueva tarea hasta que se complete la anterior

        int eventType = Random.Range(0, 3); // 0, 1 o 2
        isTaskActive = true;

        switch (eventType)
        {
            case 0:
                LaunchCleaningTask();
                break;
            case 1:
                LaunchInspectionTask();
                break;
            case 2:
                LaunchRestTask();
                break;
        }
    }

    private void LaunchCleaningTask()
    {
        cleaningTaskEvent.Raise(cleaningTask);
        Debug.Log($"[EventSystem] Cleaning task lanzada: {cleaningTask.name}");
    }

    private void LaunchInspectionTask()
    {
        inspectionTaskEvent.Raise(inspectionTask);
        Debug.Log($"[EventSystem] Inspection task lanzada: {inspectionTask.name}");
    }

    private void LaunchRestTask()
    {
        restTaskEvent.Raise(restTask);
        Debug.Log($"[EventSystem] Rest task lanzada: {restTask.name}");
    }

    // Llamar a esto desde los sistemas cuando se complete la tarea
    public void CompleteCurrentTask()
    {
        isTaskActive = false;
        Debug.Log("[EventSystem] Tarea completada. Lista para la siguiente.");
    }
}
