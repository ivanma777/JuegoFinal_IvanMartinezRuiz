using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class EventTimeLine : MonoBehaviour
{
    [SerializeField] private DayNight dayNight;  
    [SerializeField] private float intervalHours = 1f; 
    private float lastTriggerHour = -1f;
    [SerializeField] private TextMeshProUGUI clockText;

    public static event Action TimeToTriggerEvent;

    void Update()
    {
        float currentHour = dayNight.hour;

        int displayHour = Mathf.FloorToInt(currentHour);
        clockText.text = displayHour.ToString("D2") + ":00";

       
        if (lastTriggerHour < 0 || currentHour - lastTriggerHour >= intervalHours || (currentHour < lastTriggerHour && lastTriggerHour > 20))
        {
            lastTriggerHour = currentHour;
            TimeToTriggerEvent?.Invoke();
            Debug.Log("[EventTimeLine] Evento disparado a las " + currentHour + "h");
        }
    }

    public void TriggerEventManually()
    {
        TimeToTriggerEvent?.Invoke();
        Debug.Log("[EventTimeLine] Evento disparado manualmente");
    }
}
