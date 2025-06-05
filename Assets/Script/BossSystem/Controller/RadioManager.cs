using System.Collections;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private DayNight dayNight;
    [SerializeField] private RadioSO radioSO;
    [SerializeField] private TrustSystem trustSystem;
    [SerializeField] private MentalHealthEvent mentalHealthEvent;
    [SerializeField] private RadioEvent radioEvent;

    [Header("Configuración")]
    [SerializeField] private float tiempoParaResponder = 5f;
    [SerializeField] private float horasEntreAlarmas = 3f;

    private float ultimaHoraAlarma = -3f;
    private bool esperandoRespuesta = false;

    private Coroutine cuentaRegresiva;

    private void Update()
    {
        float horaActual = dayNight.hora;

        if (!esperandoRespuesta && (horaActual >= ultimaHoraAlarma + horasEntreAlarmas || horaActual < ultimaHoraAlarma))
        {
            ActivarAlarma();
            ultimaHoraAlarma = Mathf.Floor(horaActual / horasEntreAlarmas) * horasEntreAlarmas;
        }
    }

    private void ActivarAlarma()
    {
        esperandoRespuesta = true;

        int confianzaActual = trustSystem.getActualTrust();
        RadioResponseSO respuesta = radioSO.ObtenerRespuestaPorConfianza(confianzaActual);

        if (respuesta != null)
        {
            radioEvent.Raise(radioSO);
            cuentaRegresiva = StartCoroutine(EsperarRespuesta());
        }
    }

    public void CogerAlarma()
    {
        if (!esperandoRespuesta) return;

        if (cuentaRegresiva != null)
        {
            StopCoroutine(cuentaRegresiva);
            cuentaRegresiva = null;
        }

        trustSystem.ModifyTrust(+3); // Recompensa
        esperandoRespuesta = false;
        Debug.Log("[RadioManager] Alarma cogida a tiempo. Se gana confianza.");
    }

    private IEnumerator EsperarRespuesta()
    {
        yield return new WaitForSeconds(tiempoParaResponder);

        trustSystem.ModifyTrust(-5);
        mentalHealthEvent.Raise(-1);
        esperandoRespuesta = false;

        Debug.Log("[RadioManager] No se respondió a tiempo. Penalización aplicada.");
    }
}