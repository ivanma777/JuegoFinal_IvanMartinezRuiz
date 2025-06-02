using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningTaskSystem : MonoBehaviour
{
    [SerializeField] private CleaningTaskEvent cleaningEvent;
    [SerializeField] private CleaningTaskSO taskData;
    [SerializeField] private CleaningTaskView view;
    [SerializeField] private Transform dirtParent;
    [SerializeField] private VoidEvent taskCompletedEvent;

    private float remainingTime;
    private int cleanedCount = 0;
    private List<GameObject> activeDirtSpots = new();

    [SerializeField] private TrustEvent trustEvent;
    [SerializeField] private MentalHealthEvent mentalHealthEvent;

    private void OnEnable()
    {
        cleaningEvent.Register(OnCleaningTaskStarted);
    }

    private void OnDisable()
    {
        cleaningEvent.UnRegister(OnCleaningTaskStarted);
    }

    private void OnCleaningTaskStarted(CleaningTaskSO task)
    {
        taskData = task;
        CanvasManager.Instance.ShowTask(task);
        StartCleaningTask();
    }

    public void StartCleaningTask()
    {
        remainingTime = taskData.timeLimit;
        cleanedCount = 0;
        view.ShowUI();
        GenerateDirtSpots();
        StartCoroutine(TaskTimer());
    }

    private void GenerateDirtSpots()
    {
        foreach (Vector3 point in taskData.spawnPositions)
        {
            if (activeDirtSpots.Count >= taskData.numberOfDirtSpots) break;
            GameObject dirt = Instantiate(taskData.dirtPrefab, point, Quaternion.identity, dirtParent);
            dirt.GetComponent<DirtSpot>().Init(this);
            activeDirtSpots.Add(dirt);
        }
    }

    public void OnDirtCleaned(GameObject dirt)
    {
        activeDirtSpots.Remove(dirt);
        Destroy(dirt);
        cleanedCount++;

        if (cleanedCount >= taskData.numberOfDirtSpots)
        {
            TaskCompleted(true);
        }
    }

    private IEnumerator TaskTimer()
    {
        CanvasManager.Instance.StartTimer(remainingTime);
        while (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            view.UpdateTimer(remainingTime);
            CanvasManager.Instance.UpdateTimer(remainingTime);
            yield return null;
        }

        if (cleanedCount < taskData.numberOfDirtSpots)
        {
            TaskCompleted(false);
            
        }
    }

    private void TaskCompleted(bool success)
    {
        CanvasManager.Instance.StopTimer();
        StopAllCoroutines();
        view.HideUI();
        foreach (var dirt in activeDirtSpots) Destroy(dirt);
        activeDirtSpots.Clear();

        trustEvent.Raise(success ? 10 : -10);
        mentalHealthEvent.Raise(-5);
        CanvasManager.Instance.ShowResult(success, taskData);

        taskCompletedEvent.Raise(new Void());

    }
}
