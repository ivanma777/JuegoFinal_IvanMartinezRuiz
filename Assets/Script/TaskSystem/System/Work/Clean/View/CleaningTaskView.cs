using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CleaningTaskView : MonoBehaviour
{
    public GameObject taskUI;
    [SerializeField] private TMP_Text timerText;

    public void ShowUI() => taskUI.SetActive(true);
    public void HideUI() => taskUI.SetActive(false);

    public void UpdateTimer(float time)
    {
        timerText.text = $"Tiempo restante: {Mathf.CeilToInt(time)}s";
    }
}
