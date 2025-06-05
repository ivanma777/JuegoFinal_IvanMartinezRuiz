using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    [Header("Eventos")]
    [SerializeField] private CleaningTaskEvent cleaningTaskEvent;
    [SerializeField] private InspectionTaskEvent inspectionTaskEvent;
    [SerializeField] private RestTaskEvent restTaskEvent;
    [SerializeField] private VoidEvent taskCompletedEvent;

    [Header("Tareas")]
    [SerializeField] private CleaningTaskSO cleaningTask;       // Solo uno
    [SerializeField] private InspectionSO inspectionTask;       // Solo uno
    [SerializeField] private RestTaskSO restTask;               // Solo uno

    private bool isTaskActive = false;

    private enum TaskType { None, Cleaning, Inspection, Rest }
    private TaskType lastTask = TaskType.None;


    private void OnEnable()
    {
        EventTimeLine.TimeToTriggerEvent += OnTimeToTriggerEvent;
        taskCompletedEvent.Register(CompleteCurrentTask);
    }

    private void OnDisable()
    {
        EventTimeLine.TimeToTriggerEvent -= OnTimeToTriggerEvent;
        taskCompletedEvent.UnRegister(CompleteCurrentTask);
    }

    private void OnTimeToTriggerEvent()
    {
        if (isTaskActive) return; // No lanzar nueva tarea hasta que se complete la anterior

        List<TaskType> availableTasks = new List<TaskType> { TaskType.Cleaning, TaskType.Inspection, TaskType.Rest };
        availableTasks.Remove(lastTask); // Evita repetir la misma tarea

        // Seleccionar nueva tarea aleatoria
        TaskType newTask = availableTasks[Random.Range(0, availableTasks.Count)];
        isTaskActive = true;
        lastTask = newTask;


        

        switch (newTask)
        {
            case TaskType.Cleaning:
                LaunchCleaningTask();
                break;
            case TaskType.Inspection:
                LaunchInspectionTask();
                break;
            case TaskType.Rest:
                LaunchRestTask();
                break;
        }
    }

    private void LaunchCleaningTask()
    {
        Debug.Log($"[EventSystem] Cleaning task lanzada: {cleaningTask.name}");
        cleaningTaskEvent.Raise(cleaningTask);
    }

    private void LaunchInspectionTask()
    {
        Debug.Log($"[EventSystem] Inspection task lanzada: {inspectionTask.name}");
        inspectionTaskEvent.Raise(inspectionTask);
    }

    private void LaunchRestTask()
    {
        Debug.Log($"[EventSystem] Rest task lanzada: {restTask.name}");
        restTaskEvent.Raise(restTask);
    }

    // Llamar a esto desde los sistemas cuando se complete la tarea
    public void CompleteCurrentTask(Void _)
    {
        isTaskActive = false;
        Debug.Log("[EventSystem] Tarea completada. Lista para la siguiente.");

        
    }
}
