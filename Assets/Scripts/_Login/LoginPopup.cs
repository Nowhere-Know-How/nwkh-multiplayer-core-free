using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPopup : MonoBehaviour
{
    NakamaApi nakama;
    public TMP_InputField emailText;
    public TMP_InputField passwordText;

    public GameObject basePanel;
    public ToastPopup toastPanel;

    public Button loginButton;
    public Button closeButton;

    void Start()
    {
        nakama = FindObjectOfType<NakamaApi>();
        loginButton.onClick.AddListener(Login);
        closeButton.onClick.AddListener(Close);

        EventManager.onLoginAttempt.AddListener(HandleOnLoginAttempt);

    }

    private void OnDestroy()
    {
        loginButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

        EventManager.onLoginAttempt.RemoveListener(HandleOnLoginAttempt);

    }
    private void HandleOnLoginAttempt(AccountLoginResolution resolution)
    {
        switch (resolution)
        {
            case AccountLoginResolution.SUCCESS:
                toastPanel.OpenPopup();
                break;

            case AccountLoginResolution.FAILED:
                toastPanel.OpenPopup();
                break;

            default:
                throw new System.Exception("AccountCreationResolution not implemented: " + resolution.ToString());
        }

    }

    void Login()
    {
        nakama.Login(emailText.text, passwordText.text);
    }

    void Close()
    {
        basePanel.SetActive(true);
        this.gameObject.SetActive(false);

    }
}
