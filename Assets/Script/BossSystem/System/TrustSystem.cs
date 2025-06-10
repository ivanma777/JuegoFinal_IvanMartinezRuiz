using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrustSystem : MonoBehaviour
{

    public static TrustSystem Instance { get; private set; }

    [SerializeField] private int confianzaActual = 50;
    [SerializeField] private int maxConfianza = 100;
    [SerializeField] private int minConfianza = 0;

    [SerializeField] private TrustEvent trustEvent;

    public UnityEvent<int> onConfianzaActualizada;

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
        confianzaActual = Mathf.Clamp(confianzaActual + cantidad, minConfianza, maxConfianza);
        onConfianzaActualizada?.Invoke(confianzaActual);
        Debug.Log("Confianza actual: " + confianzaActual);
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

    public int getActualTrust() => confianzaActual;
}



