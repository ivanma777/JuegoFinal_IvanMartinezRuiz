using System.Collections;
using UnityEngine;
using TMPro;

public class RadioManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private DayNight dayNight;
    [SerializeField] private RadioSO radioSO;
    [SerializeField] private TrustSystem trustSystem;
    [SerializeField] private VoidEvent radioAnsweredEvent;
    [SerializeField] private RadioEvent radioEvent;
    [SerializeField] private MentalHealthEvent mentalHealthEvent;
    private AudioSource audioSource;

    [Header("Configuración")]
    [SerializeField] private float tiempoParaResponder = 5f;
    [SerializeField] private float horasEntreAlarmas = 3f;

    private bool alarmaActiva = false;
    private Coroutine cuentaRegresiva;
    private float ultimaHoraAlarma = -3f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnEnable()
    {
        radioAnsweredEvent.Register(OnRadioAnswered);
    }

    private void OnDisable()
    {
        radioAnsweredEvent.UnRegister(OnRadioAnswered);
    }

    private void Update()
    {
        float horaActual = dayNight.hora;
        if ((horaActual >= ultimaHoraAlarma + horasEntreAlarmas) || (horaActual < ultimaHoraAlarma))
        {
            Debug.Log("envioAlarma");
            audioSource.Play();
            ActivarAlarma(horaActual);
            //ultimaHoraAlarma = Mathf.Floor(horaActual / horasEntreAlarmas) * horasEntreAlarmas;
        }
    }

    private void ActivarAlarma(float horaActual)
    {
        if (alarmaActiva) return;

        alarmaActiva = true;
        ultimaHoraAlarma = Mathf.Floor(horaActual / horasEntreAlarmas) * horasEntreAlarmas;

        int confianzaActual = trustSystem.getActualTrust();
        RadioResponseSO respuesta = radioSO.ObtenerRespuestaPorConfianza(confianzaActual);

        if (respuesta != null)
        {
            radioEvent.Raise(radioSO);
            cuentaRegresiva = StartCoroutine(EsperarRespuesta());
        }
    }

    private void OnRadioAnswered(Void _)
    {
        if (!alarmaActiva) return;

        if (cuentaRegresiva != null)
        {
            StopCoroutine(cuentaRegresiva);
            cuentaRegresiva = null;
        }

        trustSystem.ModifyTrust(+5);
        ResetAlarma();
    }

    private IEnumerator EsperarRespuesta()
    {
        yield return new WaitForSeconds(tiempoParaResponder);

        trustSystem.ModifyTrust(-5);
        mentalHealthEvent.Raise(-1);
        ResetAlarma();
    }

    private void ResetAlarma()
    {
        if (cuentaRegresiva != null)
        {
            StopCoroutine(cuentaRegresiva);
            cuentaRegresiva = null;
        }
        alarmaActiva = false;
    }
}
