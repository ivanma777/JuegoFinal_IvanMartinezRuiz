using UnityEngine;
using System.Collections;

public class RestTaskSystem : MonoBehaviour
{
    [SerializeField] private RestTaskEvent restTaskEvent;
    [SerializeField] private RestEvent restEvent;
    [SerializeField] private SearchEvent searchEvent;
    [SerializeField] private VoidEvent taskCompletedEvent;
    [SerializeField] private MentalHealthEvent mentalHealthEvent; 
    [SerializeField] private DayNight dayNight;
    [SerializeField] private RestTaskSO restTaskSO;
    
    private RestZoneTrigger trigger;
    private SearchSystem search;

    private Coroutine restCoroutine;


    private void Awake()
    {
        trigger = GetComponentInChildren<RestZoneTrigger>();
        search = GetComponentInChildren<SearchSystem>();
    }

    private void OnEnable()
    {
        restTaskEvent.Register(OnRestTaskReceived);
    }

    private void OnDisable()
    {
        restTaskEvent.UnRegister(OnRestTaskReceived);
        if (restCoroutine != null)
            StopCoroutine(restCoroutine);
    }

    private void OnRestTaskReceived(RestTaskSO taskData)
    {
        restTaskSO.active = true;

        Debug.Log($"[RestTaskSystem] Activando eventos de descanso: {taskData.name}");
        restEvent.Raise(taskData);   
        searchEvent.Raise(taskData);
        CanvasManager.Instance.ActivateHUD(true);

        if (restCoroutine != null)
            StopCoroutine(restCoroutine);

        restCoroutine = StartCoroutine(RestDurationCoroutine());
    }

    private IEnumerator RestDurationCoroutine()
    {
        float startHour = dayNight.hour;
        float waitTime = 5f;
        trigger.RestEventActive = true;


        while (true)
        {
            float currentHour = dayNight.hour;

            float elapsed = currentHour >= startHour
                ? currentHour - startHour
                : (24f - startHour) + currentHour;

            if (elapsed >= waitTime)
                break;

            yield return null;
        }

        Debug.Log("[RestTaskSystem] Descanso finalizado. Volver al trabajo...");
        search.DeactivateUncollectedPapers();
        restTaskSO.active = false;
        trigger.RestEventActive = false;
        CanvasManager.Instance.ShowResult(true, restTaskSO);
        mentalHealthEvent.Raise(-1); // NUEVO: Volver al trabajo cuesta
        restCoroutine = null;
        CanvasManager.Instance.ActivateHUD(false);
        taskCompletedEvent.Raise(new Void());  // Marca la tarea como completada
    }
}