using UnityEngine;

public class RestEventSystem : MonoBehaviour
{
    [SerializeField] private RestEvent restEvent;
    [SerializeField] private GameObject restZone; // Prefab o zona que se activa
    private RestZoneTrigger restZoneTrigger;

    private void Awake()
    {
        if (restZone != null)
            restZoneTrigger = restZone.GetComponentInChildren<RestZoneTrigger>();
    }

    private void OnEnable()
    {
        restEvent.Register(OnRestEventReceived);
    }

    private void OnDisable()
    {
        restEvent.UnRegister(OnRestEventReceived);
    }

    private void OnRestEventReceived(RestTaskSO task)
    {
        restZoneTrigger.RestEventActive = task.active;

        Debug.Log($"[RestEventSystem] Activando zona de descanso: {task.name}");
        CanvasManager.Instance.ShowTask(task);

        if (restZone != null)
        {
            restZone.SetActive(true);

            // Configura la zona con los datos del ScriptableObject
            var trigger = restZoneTrigger;
            if (trigger != null)
            {
                trigger.GetType().GetField("hoursToAdvance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                       ?.SetValue(trigger, task.hoursToAdvance);

                trigger.GetType().GetField("mentalHealthGain", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                       ?.SetValue(trigger, task.mentalHealthRecovery);
            }
        }
    }
}
