using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [Header("Referencias UI")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Image eButton;
    [SerializeField] private Image InspectionButton;
    [SerializeField] private Image RestScreen;
    [SerializeField] private Animator RestScreenAnim;
    [SerializeField] private TMP_Text hudTextSearch;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private MouseScript mouseScript;

    [Header("Temporizador")]
    [SerializeField] private TMP_Text timerText; 
    private float maxTime;


    private bool openPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            openPanel = !openPanel;
            pausePanel.SetActive(openPanel);

        }
    }

    public void Return()
    {
        openPanel = !openPanel;
        pausePanel.SetActive(openPanel);
        //if (mouseScript.PanelAbierto)
        //{
        //    Time.timeScale = 1f;
        //    Cursor.lockState = CursorLockMode.None;
        //    pausePanel.SetActive(false);
        //    //mouseScript.PanelAbierto = false;
        //}

    }

    private void PauseGame()
    {


    }

    public void ShowTask(TaskSO task)
    {
        titleText.text = task.title;
        descriptionText.text = task.description;
        objectiveText.text = task.objetive;
        resultText.text = "";
        timerText.text = "";
        timerText.gameObject.SetActive(false);
    }

    public void ShowResult(bool success, TaskSO task)
    {
        resultText.text = success ? task.textReward : task.TextFail;
        timerText.gameObject.SetActive(false);
    }

    public void HideTask()
    {
        titleText.text = "";
        descriptionText.text = "";
        objectiveText.text = "";
        resultText.text = "";
        timerText.text = "";
        timerText.gameObject.SetActive(false);
    }

   

    public void StartTimer(float duration)
    {
        maxTime = duration;
        timerText.gameObject.SetActive(true);
        UpdateTimer(duration);  
    }

    public void UpdateTimer(float timeLeft)
    {
        
        timerText.text = Mathf.Ceil(timeLeft).ToString() + "s";
    }

    public void StopTimer()
    {
        timerText.gameObject.SetActive(false);
    }

    public void ShowInspection()
    {
        InspectionButton.gameObject.SetActive(true);

    }

    public void HideInspection()
    {

        InspectionButton.gameObject.SetActive(false);

    }

    public void ShowInteraction()
    {
        eButton.gameObject.SetActive(true);

    }

    public void HideInteraction()
    {

        eButton.gameObject.SetActive(false);

    }

    public void SetScreen(bool screen)
    {
        if(screen)
        {
            //RestScreen.gameObject.SetActive(true);
            RestScreenAnim.SetBool("ScreenBool", true);
            RestScreenAnim.updateMode = AnimatorUpdateMode.UnscaledTime;

        }
        else
        {

            RestScreenAnim.SetBool("ScreenBool", false);
            RestScreenAnim.updateMode = AnimatorUpdateMode.Normal;
            //RestScreen.gameObject.SetActive(false);
        }


    }

    public void ActivateHUD(bool activate)
    {
        if (activate)
        {

            hudTextSearch.gameObject.SetActive(true);


        }
        else

        {
            hudTextSearch.gameObject.SetActive(false);

        }

    }


}
