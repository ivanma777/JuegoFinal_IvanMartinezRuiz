using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;

public class RadioUI : MonoBehaviour
{
    [SerializeField] private RadioEvent radioEvent;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Text uiText;

    private void OnEnable()
    {
        radioEvent.Register(OnRadioTriggered);
    }

    private void OnDisable()
    {
        radioEvent.UnRegister(OnRadioTriggered);
    }

    private void OnRadioTriggered(RadioSO data)
    {
        if (audioSource != null && data.audioClip != null)
            audioSource.PlayOneShot(data.audioClip);

        if (uiText != null)
            uiText.text = "[Radio] Alarma: responde antes de que se enfade el jefe.";
    }
}
