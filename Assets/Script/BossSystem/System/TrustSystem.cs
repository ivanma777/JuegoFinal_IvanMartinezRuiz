using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrustSystem : MonoBehaviour
{

    public static TrustSystem Instance { get; private set; }

    [SerializeField] private int currentConfidence = 100;
    [SerializeField] private int maxConfidence = 200;
    [SerializeField] private int minConfidence = 0;

    [SerializeField] private TrustEvent trustEvent;

    public UnityEvent<int> onActualConfidence;

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

    public void ModifyTrust(int cantidad)
    {
        currentConfidence = Mathf.Clamp(currentConfidence + cantidad, minConfidence, maxConfidence);
        onActualConfidence?.Invoke(currentConfidence);
        Debug.Log("Confianza actual: " + currentConfidence);
    }

    private void OnEnable()
    {
        if (trustEvent != null)
            trustEvent.Register(ModifyTrust);

    }

    private void OnDisable()
    {
        if (trustEvent != null)
            trustEvent.UnRegister(ModifyTrust);
    }

    public int getActualTrust() => currentConfidence;
}



