using Nakama;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSceneControls : MonoBehaviour
{
    public string firstSceneOnNewAccount = "Kerfuffle";
    public string mainMenuScene = "1-MainMenu";
    NakamaApi nakama;

    void Awake()
    {
        nakama = FindObjectOfType<NakamaApi>();

        EventManager.onServerDiscovery.AddListener(SwitchScenesOnServerDiscovery); //This only runs locally before we connect
        EventManager.onLoginAttempt.AddListener(GetLoginInformation); //This only runs locally before we connect
        EventManager.onGetMatchId.AddListener(JoinNetworkScene);
        EventManager.onRoomJoin.AddListener(SwitchNetworkScenes);
        EventManager.onGetLoginInformation.AddListener(FinallyLogin);
    }

    private void OnDestroy()
    {
        EventManager.onServerDiscovery.RemoveAllListeners();
        EventManager.onLoginAttempt.RemoveAllListeners();
        EventManager.onGetMatchId.RemoveAllListeners();
        EventManager.onRoomJoin.RemoveAllListeners();
        EventManager.onGetLoginInformation.RemoveAllListeners();
    }

    void SwitchScenesOnServerDiscovery()
    {
        Debug.Log("Discovered");
        SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Single);
    }

    void GetLoginInformation(AccountLoginResolution resolution)
    {
        if (resolution == AccountLoginResolution.SUCCESS)
        {
            SceneManager.LoadScene("_GlobalObjects", LoadSceneMode.Additive);
            nakama.GetLoginInfo();
        }
    }

    void FinallyLogin(PlayerDataResponse response)
    {
        string nextScene;
        if (response.scene == null)
        {
            nextScene = firstSceneOnNewAccount;
        }
        else
        {
            nextScene = response.scene;
        }
        WarpGateData.nextSceneName = nextScene;
        StartCoroutine(nakama.ClientJoinMatchByMatchId(nextScene));
    }

    void SwitchNetworkScenes()
    {
        //Debug.Log(WarpGateData.nextSceneName);
        SceneManager.LoadScene(WarpGateData.nextSceneName, LoadSceneMode.Single);
    }

    void JoinNetworkScene(MatchJoinResponse response)
    {
        string matchId = response.payload;
        Debug.Log("Joining room: " + matchId);
        nakama.JoinMatchIdAsync(matchId);
    }

}
