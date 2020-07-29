using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegisterPopup : MonoBehaviour
{
    NakamaApi nakama;
    public TMP_InputField emailText;
    public TMP_InputField nameText;
    public TMP_InputField passwordText;
    public TMP_InputField passwordText2;

    public GameObject basePanel;
    public ToastPopup toastPanel;

    public Button registerButton;
    public Button closeButton;

    void Start()
    {
        nakama = FindObjectOfType<NakamaApi>();
        registerButton.onClick.AddListener(Register);
        closeButton.onClick.AddListener(Close);
        EventManager.onAccountCreation.AddListener(HandleOnAccountCreation);
    }

    private void OnDestroy()
    {
        registerButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        EventManager.onAccountCreation.RemoveListener(HandleOnAccountCreation);
    }

    void HandleOnAccountCreation(AccountRegisterResolution resolution)
    {
        switch (resolution)
        {
            case AccountRegisterResolution.SUCCESS:
                toastPanel.OpenPopup();
                this.gameObject.SetActive(false);
                basePanel.SetActive(true);
                break;

            case AccountRegisterResolution.FAILED:
                toastPanel.OpenPopup();

                break;

            default:
                throw new System.Exception("AccountCreationResolution not implemented: " + resolution.ToString());
        }

    }

    void Register()
    {
        if (emailText.text.Length == 0 || passwordText.text.Length == 0 || passwordText2.text.Length == 0 || nameText.text.Length == 0)
        {
            Debug.Log("Form incomplete. Please fill out all the fields.");
            toastPanel.OpenPopup("Form incomplete. Please fill out all the fields.");
            return;
        }

        if (passwordText.text.Length < 8)
        {
            Debug.Log("Password must be at least 8 characters.");
            toastPanel.OpenPopup("Password must be at least 8 characters.");
            return;
        }

        if (passwordText.text != passwordText2.text)
        {
            Debug.Log("Password doesn't match. Please try again.");
            toastPanel.OpenPopup("Password doesn't match. Please try again.");
            return;
        }
        for (int i = 0; i < emailText.text.Length; i++)
        {
            Debug.Log(emailText.text[i]);
        }
        Debug.Log("Register: " + emailText.text + ":" + passwordText.text + ":" + passwordText2.text);
        nakama.Register(nameText.text, emailText.text, passwordText.text, PlayerGender.MALE);
    }

    void Close()
    {
        basePanel.SetActive(true);
        this.gameObject.SetActive(false);

    }
}
