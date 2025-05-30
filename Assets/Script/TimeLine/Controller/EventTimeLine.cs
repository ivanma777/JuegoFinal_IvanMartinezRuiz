using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventTimeLine : MonoBehaviour
{
    [SerializeField] private DayNight dayNight;  // Referencia al ciclo día-noche
    [SerializeField] private float intervalHours = 1f; // Cada cuántas horas lanzar evento
    private float lastTriggerHour = -1f;

    public static event Action TimeToTriggerEvent;

    void Update()
    {
        float currentHour = dayNight.hora;

        // Lanzar evento si hemos avanzado más de intervalHours desde el último disparo
        if (lastTriggerHour < 0 || currentHour - lastTriggerHour >= intervalHours || (currentHour < lastTriggerHour && lastTriggerHour > 20))
        {
            lastTriggerHour = currentHour;
            TimeToTriggerEvent?.Invoke();
            Debug.Log("[EventTimeLine] Evento disparado a las " + currentHour + "h");
        }
    }

    // Método para lanzar el evento manualmente
    public void TriggerEventManually()
    {
        TimeToTriggerEvent?.Invoke();
        Debug.Log("[EventTimeLine] Evento disparado manualmente");
    }
}
