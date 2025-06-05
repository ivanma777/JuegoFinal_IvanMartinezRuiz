using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchSystem : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private SearchEvent searchEvent;
    [SerializeField] private RestTaskSO taskData;
    [SerializeField] private TMP_Text hudText;
    [SerializeField] private GameObject finalMessage;
    [SerializeField] private Transform paperParent;
    [SerializeField] private GameObject paperPrefab;
    [SerializeField] private List<Transform> spawnPoints;

    private int papersCollected = 0;
    private int totalPapers = 0;
    private bool finalSecretUnlocked = false;

    private List<PaperCollect> activePapers = new();
    private Queue<PaperCollect> pool = new();

    private HashSet<int> collectedPaperIndices = new();

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
        taskData = task;
        totalPapers = task.totalPapers;
        
        finalSecretUnlocked = false;
        GeneratePapers();
        UpdateHUD();
    }

    private void GeneratePapers()
    {
        ClearExistingPapers();
        int generated = 0;

        for (int i = 0; i < spawnPoints.Count && generated < taskData.totalPapers; i++)
        {
            if (collectedPaperIndices.Contains(i))
                continue; // No generamos papeles ya recogidos

            PaperCollect paper = GetFromPool();
            Transform spawn = spawnPoints[i];
            paper.transform.SetParent(paperParent);
            paper.transform.position = spawn.position;
            paper.transform.rotation = spawn.rotation;
            paper.Init(this, i); // <-- Pasamos el índice
            paper.gameObject.SetActive(true);
            activePapers.Add(paper);

            generated++;
        }
    }

    private PaperCollect GetFromPool()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }
        else
        {
            GameObject newObj = Instantiate(taskData.PaperPrefab, Vector3.zero, Quaternion.identity);
            return newObj.GetComponent<PaperCollect>();
        }
    }

    public void CollectPaper(PaperCollect paper, int index)
    {
        paper.gameObject.SetActive(false);
        activePapers.Remove(paper);
        pool.Enqueue(paper);

        collectedPaperIndices.Add(index); // Marcar como recogido
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

    private void ClearExistingPapers()
    {
        foreach (var paper in activePapers)
        {
            paper.gameObject.SetActive(false);
            pool.Enqueue(paper);
        }
        activePapers.Clear();
    }

    public void DeactivateUncollectedPapers()
    {
        foreach (var paper in activePapers)
        {
            paper.gameObject.SetActive(false);
            pool.Enqueue(paper);
        }
        activePapers.Clear();
    }
}
