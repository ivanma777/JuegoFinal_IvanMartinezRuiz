using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventTimeLine : MonoBehaviour
{
    [SerializeField] private DayNight dayNight;  // Referencia al ciclo d�a-noche
    [SerializeField] private float intervalHours = 1f; // Cada cu�ntas horas lanzar evento
    private float lastTriggerHour = -1f;

    public static event Action TimeToTriggerEvent;

    void Update()
    {
        float currentHour = dayNight.hora;

        // Lanzar evento si hemos avanzado m�s de intervalHours desde el �ltimo disparo
        if (lastTriggerHour < 0 || currentHour - lastTriggerHour >= intervalHours || (currentHour < lastTriggerHour && lastTriggerHour > 20))
        {
            lastTriggerHour = currentHour;
            TimeToTriggerEvent?.Invoke();
            Debug.Log("[EventTimeLine] Evento disparado a las " + currentHour + "h");
        }
    }

    // M�todo para lanzar el evento manualmente
    public void TriggerEventManually()
    {
        TimeToTriggerEvent?.Invoke();
        Debug.Log("[EventTimeLine] Evento disparado manualmente");
    }
}
