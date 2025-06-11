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
    [SerializeField] private float timeToAnswer = 5f;
    [SerializeField] private float timeBetweenAlarm = 3f;

    private bool activeAlarm = false;
    private Coroutine countDown;
    private float lastHourAlarm = -3f;

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
        float horaActual = dayNight.hour;
        if ((horaActual >= lastHourAlarm + timeBetweenAlarm) || (horaActual < lastHourAlarm))
        {
            Debug.Log("envioAlarma");
            audioSource.Play();
            ActiveAlarm(horaActual);
            //ultimaHoraAlarma = Mathf.Floor(horaActual / horasEntreAlarmas) * horasEntreAlarmas;
        }
    }

    private void ActiveAlarm(float horaActual)
    {
        if (activeAlarm) return;

        activeAlarm = true;
        lastHourAlarm = Mathf.Floor(horaActual / timeBetweenAlarm) * timeBetweenAlarm;

        int confianzaActual = trustSystem.getActualTrust();
        RadioResponseSO respuesta = radioSO.AnswerByTrust(confianzaActual);

        if (respuesta != null)
        {
            radioEvent.Raise(radioSO);
            countDown = StartCoroutine(WaitAnswer());
        }
    }

    private void OnRadioAnswered(Void _)
    {
        if (!activeAlarm) return;

        if (countDown != null)
        {
            StopCoroutine(countDown);
            countDown = null;
        }

        trustSystem.ModifyTrust(+5);
        ResetAlarma();
    }

    private IEnumerator WaitAnswer()
    {
        yield return new WaitForSeconds(timeToAnswer);

        trustSystem.ModifyTrust(-5);
        mentalHealthEvent.Raise(-1);
        ResetAlarma();
    }

    private void ResetAlarma()
    {
        if (countDown != null)
        {
            StopCoroutine(countDown);
            countDown = null;
        }
        activeAlarm = false;
    }
}
