using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningTaskSystem : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private CleaningTaskEvent cleaningEvent;
    [SerializeField] private CleaningTaskSO taskData;
    [SerializeField] private CleaningTaskView view;
    [SerializeField] private Transform dirtParent;
    [SerializeField] private VoidEvent taskCompletedEvent;

    [Header("Eventos de confianza y salud mental")]
    [SerializeField] private TrustEvent trustEvent;
    [SerializeField] private MentalHealthEvent mentalHealthEvent;

    [Header("Spawn Points")]
    [SerializeField] private List<Transform> spawnPoints; 

    private float remainingTime;
    private int cleanedCount = 0;

    private List<DirtSpot> activeDirtSpots = new();
    private Queue<DirtSpot> pool = new();

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
        ClearExistingSpots();

        int count = Mathf.Min(taskData.numberOfDirtSpots, spawnPoints.Count);

        for (int i = 0; i < count; i++)
        {
            DirtSpot dirt = GetFromPool();
            Transform spawn = spawnPoints[i];
            dirt.transform.SetParent(dirtParent);
            dirt.transform.position = spawn.position;
            dirt.transform.rotation = spawn.rotation;
            dirt.Init(this);
            dirt.gameObject.SetActive(true);
            activeDirtSpots.Add(dirt);
        }
    }

    private DirtSpot GetFromPool()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }
        else
        {
            GameObject newObj = Instantiate(taskData.dirtPrefab, Vector3.zero, Quaternion.identity);
            return newObj.GetComponent<DirtSpot>();
        }
    }

    public void OnDirtCleaned(DirtSpot dirt)
    {
        dirt.gameObject.SetActive(false);
        activeDirtSpots.Remove(dirt);
        pool.Enqueue(dirt);
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
        ClearExistingSpots();

        trustEvent.Raise(success ? 10 : -10);
        mentalHealthEvent.Raise(-5);
        CanvasManager.Instance.ShowResult(success, taskData);
        taskCompletedEvent.Raise(new Void());
    }

    private void ClearExistingSpots()
    {
        foreach (var dirt in activeDirtSpots)
        {
            dirt.gameObject.SetActive(false);
            pool.Enqueue(dirt);
        }
        activeDirtSpots.Clear();
    }
}