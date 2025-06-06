using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform playerBody;


    float xRotation = 0f;
    private bool panelAbierto = true;

    public bool PanelAbierto { get => panelAbierto; set => panelAbierto = value; }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(panelAbierto)
        {
            if(Cursor.visible)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

            }

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 
        playerBody.Rotate(Vector3.up * mouseX);

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            panelAbierto = !panelAbierto;
            //panelInspeccion.SetActive(panelAbierto);
            //camara.puedeMoverCamara = !panelAbierto;

            // Mostrar cursor
            Cursor.lockState = panelAbierto ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !panelAbierto;
        }
    }
}
