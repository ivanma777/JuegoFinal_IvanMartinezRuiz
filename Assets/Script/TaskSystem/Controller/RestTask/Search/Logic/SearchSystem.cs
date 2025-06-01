using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchSystem : MonoBehaviour
{
    [SerializeField] private SearchEvent searchEvent;
    [SerializeField] private GameObject[] paperObjects;
    [SerializeField] private Text hudText;
    [SerializeField] private GameObject finalMessage;

    private int papersCollected = 0;
    private int totalPapers = 0;
    private bool finalSecretUnlocked = false;

    private void OnEnable()
    {
        searchEvent.Register(OnSearchTaskReceived);
    }

    private void OnDisable()
    {
        searchEvent.UnRegister(OnSearchTaskReceived);
    }

    private void OnSearchTaskReceived(RestTaskSO task)
    {
        papersCollected = 0;
        finalSecretUnlocked = false;
        totalPapers = task.totalPapers;

        foreach (var paper in paperObjects)
            paper.SetActive(true);

        UpdateHUD();
    }

    public void CollectPaper(GameObject paper)
    {
        paper.SetActive(false);
        papersCollected++;
        UpdateHUD();

        if (papersCollected >= totalPapers && !finalSecretUnlocked)
        {
            UnlockFinalSecret();
        }
    }

    private void UpdateHUD()
    {
        if (hudText != null)
            hudText.text = $"Papeles encontrados: {papersCollected}/{totalPapers}";
    }

    private void UnlockFinalSecret()
    {
        finalSecretUnlocked = true;
        if (finalMessage != null)
            finalMessage.SetActive(true);

        PlayerPrefs.SetInt("FinalSecretoActivado", 1);
        Debug.Log("[SearchSystem] ¡Final secreto desbloqueado!");
    }
}
