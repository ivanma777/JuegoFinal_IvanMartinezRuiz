using UnityEngine;

public class RestTaskSystem : MonoBehaviour
{
    [SerializeField] private RestTaskEvent restTaskEvent;
    [SerializeField] private RestEvent restEvent;
    //[SerializeField] private SearchEvent searchEvent;

    private void OnEnable()
    {
        restTaskEvent.Register(OnRestTaskReceived);
    }

    private void OnDisable()
    {
        restTaskEvent.UnRegister(OnRestTaskReceived);
    }

    private void OnRestTaskReceived(RestTaskSO taskData)
    {
        Debug.Log($"[RestTaskSystem] Activando eventos de descanso: {taskData.name}");
        restEvent.Raise(taskData);   // Lanza el evento de descanso
        //searchEvent.Raise(taskData); // Lanza otro subevento posible
    }
}
