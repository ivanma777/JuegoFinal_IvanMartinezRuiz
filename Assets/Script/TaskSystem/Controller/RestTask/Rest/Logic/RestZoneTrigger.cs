using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestZoneTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private GameObject restUI;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float hoursToAdvance = 2f;
    [SerializeField] private float screenTime = 2f;
    [SerializeField] private int mentalHealthGain = 10;
    [SerializeField] private DayNight dayNight;

    private bool restEventActive; 

    [SerializeField] private MentalHealthEvent mentalHealthEvent;

    private Coroutine currentTaskCoroutine;

    private bool isPlayerInside = false;

    public bool RestEventActive { get => restEventActive; set => restEventActive = value; }

    private void Start()
    {
        if (restUI != null) restUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && restEventActive)
        {
            isPlayerInside = true;
            if (restUI != null) restUI.SetActive(true);
            timerText.text = "Descansar e";
            //restEventActive = true;
            
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInside = false;
            if (restUI != null) restUI.SetActive(false);
            //restEventActive = false;
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            Rest();
        }
    }

    private void Rest()
    {
        // Aumenta salud mental
        mentalHealthEvent.Raise(mentalHealthGain);

        if (currentTaskCoroutine != null)
            StopCoroutine(currentTaskCoroutine);

        StartCoroutine(TaskTimer());

      

        // Avanza el tiempo (aquí puedes conectar con tu sistema de tiempo)
        //TimeSystem.Instance.AdvanceTime(hoursToAdvance); //  solo si tienes un sistema de tiempo

        Debug.Log("[RestZoneTrigger] Descansando...");

        if (restUI != null) restUI.SetActive(false);
        isPlayerInside = false;
        // Puedes desactivar el collider si solo se puede usar una vez
        // gameObject.SetActive(false);
    }

    private IEnumerator TaskTimer()
    {
        CanvasManager.Instance.SetScreen(true);


        float screenTimeV2 = screenTime;

        Time.timeScale = 0f;

        // 3. Esperar 2 segundos reales (sin depender de Time.timeScale)
        
        while (screenTimeV2 > 0f)
        {
            screenTimeV2 -= Time.unscaledDeltaTime;
            yield return null;
        }

        // 4. Reanudar el juego
        Time.timeScale = 1f;





        CanvasManager.Instance.SetScreen(false);
        dayNight.hora += 2;

        currentTaskCoroutine = null;

    }
}
