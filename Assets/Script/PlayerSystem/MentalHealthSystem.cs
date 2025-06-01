using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MentalHealthSystem : MonoBehaviour
{
    public static MentalHealthSystem Instance { get; private set; }

    [SerializeField] private int saludMentalActual = 50;
    [SerializeField] private int maxSaludMental = 100;
    [SerializeField] private int minSaludMental = 0;

    public UnityEvent<int> onSaludMentalActualizada;

    [SerializeField] private MentalHealthEvent mentalHealthEvent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ModifyMentalHealth(int cantidad)
    {
        saludMentalActual = Mathf.Clamp(saludMentalActual + cantidad, minSaludMental, maxSaludMental);
        onSaludMentalActualizada?.Invoke(saludMentalActual);
        Debug.Log("Salud mental actual: " + saludMentalActual);
    }


    private void OnEnable()
    {
        if (mentalHealthEvent != null)
            mentalHealthEvent.Register(ModifyMentalHealth);
        EventTimeLine.TimeToTriggerEvent += OnHourPassed;

    }

    private void OnDisable()
    {
        if (mentalHealthEvent != null)
            mentalHealthEvent.UnRegister(ModifyMentalHealth);
        EventTimeLine.TimeToTriggerEvent -= OnHourPassed;
    }

    private void OnHourPassed()
    {
        ModifyMentalHealth(-5);
        Debug.Log("[MentalHealthTick] Salud mental reducida en 5 por el paso del tiempo.");
    }
    public int GetActualMentalHealth() => saludMentalActual;
}
