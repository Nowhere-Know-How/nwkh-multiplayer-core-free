using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToastPopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Button closeButton;

    void Start()
    {
        closeButton.onClick.AddListener(ClosePopup);
    }

    void OnDestroy()
    {
        closeButton.onClick.RemoveAllListeners();
    }

    public void OpenPopup(string message)
    {
        text.text = message;
        this.gameObject.SetActive(true);
    }

    public void OpenPopup()
    {
        text.text = DebugInfo.Message;
        this.gameObject.SetActive(true);
    }

    public void ClosePopup()
    {
        this.gameObject.SetActive(false);
    }
}
