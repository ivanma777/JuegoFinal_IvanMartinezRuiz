using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
     [SerializeField]private Light flashlight;
    //[SerializeField] private AudioSource clickSound; // Opcional, para realismo
    private bool isOn;

    void Start()
    {
        if (flashlight == null)
            flashlight = GetComponentInChildren<Light>();

        flashlight.enabled = isOn;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;

            //if (clickSound != null)
            //    clickSound.Play();
        }
    }
}
