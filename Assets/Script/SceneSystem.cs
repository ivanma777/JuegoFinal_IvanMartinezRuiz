using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSystem : MonoBehaviour
{

    


    bool pause;

    [SerializeField] GameObject MenuPause;

    [SerializeField] Animator transitionAnim;

    [SerializeField] GameObject image;

    AudioSource audioSource;

    [SerializeField] AudioSource audioEntrada;


    [SerializeField] TrustSystem trust;

    private int confianzaActual;

    private void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }
    // Start is called before the first frame update
    public void PlayTutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
    public void PlayJuego()
    {
        audioEntrada.Stop();

        SceneManager.LoadScene(1);




    }

    public void Exit()
    {
        Application.Quit();

    }

    public void Pause()
    {
        if (pause )
        {

            Cursor.lockState = CursorLockMode.None;
            MenuPause.SetActive(true);
            Time.timeScale = 0f;
        }

    } 
    public void Return()
    {
        if (!pause)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            MenuPause.SetActive(false);   
        }

    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {

            Pause();
            Return();
            if (pause)
            {
                pause = false;
            }
            else
            {

                pause = true;
            }
           
        }
        int confianza = trust.getActualTrust();

        if (confianza == 0)
        {

            SceneManager.LoadScene(0);


        }

    }

    public void Final()
    {

        SceneManager.LoadScene(1);
        //RegalosRecogidos.SetText("Regalos totales recogidos: " + Regalos);


    }
    //IEnumerator ChangeFinal()
    //{
    //    float timer = 0f;
    //    float WaitTime = 5f;

    //    image.SetActive(true);

    //    while (timer < WaitTime)
    //    {
    //        if (Input.GetKeyDown(KeyCode.X))
    //        {

    //            break;

    //        }

    //        timer += Time.deltaTime;
    //        yield return null;
    //    }

    //    yield return null;

    //    SceneManager.LoadScene(2);

    //}

}
