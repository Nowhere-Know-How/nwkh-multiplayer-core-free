using Nakama;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControls : MonoBehaviour
{
    NakamaApi nakama;

    void Start()
    {
        nakama = FindObjectOfType<NakamaApi>();

        EventManager.onGetMatchId.AddListener(JoinNetworkScene);
        EventManager.onRoomJoin.AddListener(SwitchNetworkScenes);
    }

    private void OnDestroy()
    {
        EventManager.onGetMatchId.RemoveAllListeners();
        EventManager.onRoomJoin.RemoveAllListeners();
    }

    void SwitchNetworkScenes()
    {
        SceneManager.LoadScene(WarpGateData.nextSceneName, LoadSceneMode.Single);
    }

    void JoinNetworkScene(MatchJoinResponse response)
    {
        string matchId = response.payload;
        Debug.Log("Joining room: " + matchId);
        nakama.JoinMatchIdAsync(matchId);
    }

    
}
