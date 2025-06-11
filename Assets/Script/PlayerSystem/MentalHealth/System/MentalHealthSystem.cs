using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MentalHealthSystem : MonoBehaviour
{
    public static MentalHealthSystem Instance { get; private set; }

    [SerializeField] private int currentMentalHealth = 50;
    [SerializeField] private int maxMentalHealth = 100;
    [SerializeField] private int minMentalHealth = 0;

    public UnityEvent<int> onCurrentMentalHealth;

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
        currentMentalHealth = Mathf.Clamp(currentMentalHealth + cantidad, minMentalHealth, maxMentalHealth);
        onCurrentMentalHealth?.Invoke(currentMentalHealth);
        Debug.Log("Salud mental actual: " + currentMentalHealth);
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
    public int GetActualMentalHealth() => currentMentalHealth;
}
