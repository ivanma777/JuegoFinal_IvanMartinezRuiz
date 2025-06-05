using UnityEngine;
using UnityEngine.UI;

public class ToggleInventoryPanelButton : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private bool isOpen = false;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TogglePanel);
    }

    private void TogglePanel()
    {
        isOpen = !isOpen;
        panel.SetActive(isOpen);
    }
}
