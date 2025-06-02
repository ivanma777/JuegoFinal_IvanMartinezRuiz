using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestZoneTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private GameObject restUI;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float hoursToAdvance = 2f;
    [SerializeField] private int mentalHealthGain = 10;

    [SerializeField] private MentalHealthEvent mentalHealthEvent;

    private bool isPlayerInside = false;

    private void Start()
    {
        if (restUI != null) restUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInside = true;
            if (restUI != null) restUI.SetActive(true);
            timerText.text = "Descansar";
            
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInside = false;
            if (restUI != null) restUI.SetActive(false);
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

        // Avanza el tiempo (aquí puedes conectar con tu sistema de tiempo)
        //TimeSystem.Instance.AdvanceTime(hoursToAdvance); //  solo si tienes un sistema de tiempo

        Debug.Log("[RestZoneTrigger] Descansando...");

        if (restUI != null) restUI.SetActive(false);
        isPlayerInside = false;
        // Puedes desactivar el collider si solo se puede usar una vez
        // gameObject.SetActive(false);
    }
}
