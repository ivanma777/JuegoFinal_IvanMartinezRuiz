using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    [SerializeField] private CleaningTaskEvent cleaningTaskEvent;
    [SerializeField] private InspectionTaskEvent inspectionTaskEvent;
    [SerializeField] private RestTaskEvent restTaskEvent;

    [SerializeField] private CleaningTaskSO[] cleaningTasks;
    [SerializeField] private InspectionSO[] inspectionTasks;
    [SerializeField] private RestTaskSO[] restTasks;

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
        // Aquí decides qué tipo de evento lanzar, puede ser aleatorio o basado en lógica
        int eventType = Random.Range(0, 2);
        switch (eventType)
        {
            case 0:
                LaunchRandomCleaningTask();
                break;
            case 1:
                LaunchRandomInspectionTask();
                break;
            case 2:
                LaunchRandomRestTask();
                break;
        }
    }

    private void LaunchRandomCleaningTask()
    {
        if (cleaningTasks.Length == 0) return;
        var task = cleaningTasks[Random.Range(0, cleaningTasks.Length)];
        cleaningTaskEvent.Raise(task);
        Debug.Log($"[EventSystem] Cleaning task lanzada: {task.name}");
    }

    private void LaunchRandomInspectionTask()
    {
        if (inspectionTasks.Length == 0) return;
        var task = inspectionTasks[Random.Range(0, inspectionTasks.Length)];
        inspectionTaskEvent.Raise(task);
        Debug.Log($"[EventSystem] Inspection task lanzada: {task.name}");
    }

    private void LaunchRandomRestTask()
    {
        if (restTasks.Length == 0) return;
        var task = restTasks[Random.Range(0, restTasks.Length)];
        restTaskEvent.Raise(task);
        Debug.Log($"[EventSystem] Rest task lanzada: {task.name}");
    }
}
