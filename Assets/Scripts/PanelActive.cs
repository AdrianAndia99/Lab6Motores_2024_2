using UnityEngine;
using UnityEngine.Events;

public class PanelActive : MonoBehaviour
{
    public GameObject panel;
    public UnityEvent onPanelToggle;

    private void Awake()
    {
        if (onPanelToggle == null)
        {
            onPanelToggle = new UnityEvent();
        }

        onPanelToggle.AddListener(TogglePanel);
    }

    public void TriggerPanelToggle()
    {
        onPanelToggle.Invoke();
    }

    private void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }
    public void ClosePanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
    }
}