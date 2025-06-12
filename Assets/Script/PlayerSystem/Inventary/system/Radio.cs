using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.HighDefinition;

using UnityEngine;
using TMPro;

public class Radio : MonoBehaviour
{
    [SerializeField] private RadioEvent radioEvent;
    [SerializeField] private VoidEvent radioAnsweredEvent;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private GameObject radioCanvas;
    [SerializeField] private Material radioMaterial;
    [SerializeField] private GameObject radioObject;

    private string[] lineas;
    private int index;
    private float textSpeed = 0.05f;
    private bool isActive;
    private bool hasAnswer = false;
    private bool radioOut = false;

    private AudioClip currentAudioClip;

    private void Awake()
    {
           
    }


    private void OnEnable()
    {
        radioEvent.Register(OnMessageGet);
    }

    private void OnDisable()
    {
        radioEvent.UnRegister(OnMessageGet);
    }

    private void OnMessageGet(RadioSO so)
    {
        Debug.Log("Llego alarma");
        RadioResponseSO respuesta = so.AnswerByTrust(0);

        if (respuesta != null)
        {
            lineas = new string[] { respuesta.mensaje }; 
            index = 0;
            textComponent.text = "";

            currentAudioClip = respuesta.audioClip;

           
            isActive = true;
            hasAnswer = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            radioOut = !radioOut; 

            radioObject.SetActive(radioOut);
            radioCanvas.SetActive(radioOut); 
            radioMaterial.SetColor("_EmissionColor", radioOut ? Color.white : Color.black);
        }

        if (radioOut && isActive && Input.GetKeyDown(KeyCode.R))
        {
            if (!hasAnswer)
            {
                StartCoroutine(TypeLine());
                audioSource.PlayOneShot(currentAudioClip);

                radioCanvas.SetActive(true);
                radioMaterial.EnableKeyword("_EMISSION");
                hasAnswer = true;
                radioAnsweredEvent.Raise(new Void());
            }
            else
            {
                // Apagar radio
                audioSource.Stop();
                radioCanvas.SetActive(false);
                radioMaterial.DisableKeyword("_EMISSION");
                isActive = false;
                textComponent.text = "";
            }
        }

        if (radioOut && isActive && Input.GetKeyDown(KeyCode.T))
        {
            if (lineas == null || lineas.Length == 0) return;

            if (textComponent.text == lineas[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lineas[index];
            }
        }
    }

    private IEnumerator TypeLine()
    {
        textComponent.text = "";
        foreach (char c in lineas[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private IEnumerator CloseDelay()
    {
        yield return new WaitForSeconds(1f);
        textComponent.text = "";
        radioCanvas.SetActive(false);
        radioMaterial.DisableKeyword("_EMISSION");
        radioObject.SetActive(false);
        isActive = false;
    }

    private void NextLine()
    {
        if (index < lineas.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            StartCoroutine(CloseDelay());
        }
    }
}

