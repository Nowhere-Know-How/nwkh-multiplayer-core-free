using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NakamaLoginPopup : MonoBehaviour
{
    public TMP_InputField _inputFieldAccount;
    public TMP_InputField _inputFieldPassword;

    NakamaApi nakama;

    private void Start()
    {
        nakama = FindObjectOfType<NakamaApi>();
        EventManager.onLoginAttempt.AddListener(HandleOnLoginAttempt);

    }
    private void HandleOnLoginAttempt(AccountLoginResolution resolution)
    {
        switch (resolution)
        {
            case AccountLoginResolution.SUCCESS:
                break;

            case AccountLoginResolution.FAILED:
                break;

            default:
                throw new System.Exception("AccountCreationResolution not implemented: " + resolution.ToString());
        }

    }

    public void Login()
    {
        nakama.Login(_inputFieldAccount.text, _inputFieldPassword.text);
    }

    public void LoginBrandon()
    {
        nakama.Login("brandon@gmail.com", "qweasdqweasd");
    }

    public void LoginHelen()
    {
        nakama.Login("helen@gmail.com", "qweasdqweasd");
    }

}
