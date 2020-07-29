using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMenuPopup : MonoBehaviour
{
    public Button loginButton;
    public Button registerButton;

    public GameObject loginPopup;
    public GameObject registerPopup;

    void Start()
    {
        loginButton.onClick.AddListener(ActivateLoginPopup);
        registerButton.onClick.AddListener(ActivateRegisterPopup);
    }

    void ActivateLoginPopup()
    {
        registerPopup.SetActive(false);
        loginPopup.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void ActivateRegisterPopup()
    {
        registerPopup.SetActive(true);
        loginPopup.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        loginButton.onClick.RemoveAllListeners();
        registerButton.onClick.RemoveAllListeners();
    }
}
