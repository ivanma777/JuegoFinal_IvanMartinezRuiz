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

    private Coroutine restCoroutine;

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
        Debug.Log($"[RestTaskSystem] Activando eventos de descanso: {taskData.name}");
        restEvent.Raise(taskData);   
        searchEvent.Raise(taskData); 

        if (restCoroutine != null)
            StopCoroutine(restCoroutine);

        restCoroutine = StartCoroutine(RestDurationCoroutine());
    }

    private IEnumerator RestDurationCoroutine()
    {
        float startHour = dayNight.hora;
        float waitTime = 2f;

        while (true)
        {
            float currentHour = dayNight.hora;

            float elapsed = currentHour >= startHour
                ? currentHour - startHour
                : (24f - startHour) + currentHour;

            if (elapsed >= waitTime)
                break;

            yield return null;
        }

        Debug.Log("[RestTaskSystem] Descanso finalizado. Volver al trabajo...");
        CanvasManager.Instance.ShowResult(true, restTaskSO);
        mentalHealthEvent.Raise(-1); // NUEVO: Volver al trabajo cuesta
        taskCompletedEvent.Raise(new Void());  // Marca la tarea como completada
        restCoroutine = null;
    }
}