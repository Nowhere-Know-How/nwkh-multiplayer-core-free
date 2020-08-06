using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VivoxUnity;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class VivoxControls : MonoBehaviour
{
    public bool demo = false;

    string _sceneName;
    VivoxVoiceManager _vivoxVoiceManager;
    VivoxNetworkManager _vivoxNetworkManager;
    string _currentChannel = null;

    private void Start()
    {
        _vivoxVoiceManager = GetComponentInChildren<VivoxVoiceManager>();
        _vivoxNetworkManager = GetComponentInChildren<VivoxNetworkManager>();
        if (demo)
            return;

        Login();
        SceneManager.sceneLoaded += OnSceneLoad;
        
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _sceneName = scene.name;
        JoinChannelBySceneName();
    }

    void JoinChannelBySceneName()
    {
        if (_vivoxVoiceManager.LoginState == LoginState.LoggedIn)
        {
            JoinLobbyChannel(_sceneName);
        }
        else
        {
            Invoke("JoinChannelBySceneName", 0.5f);
        }
    }


    public void JoinLobbyChannel(string channelName)
    {
        Debug.Log("Attempting to join: " + channelName);
        if (_currentChannel != null)
        {
            return;
        }

        _currentChannel = channelName;
        var lobbychannel = _vivoxVoiceManager.ActiveChannels.FirstOrDefault(ac => ac.Channel.Name == _vivoxNetworkManager.LobbyChannelName);
        if ((_vivoxVoiceManager && _vivoxVoiceManager.ActiveChannels.Count == 0)
            || lobbychannel == null)
        {
            Debug.Log("I'm in");
            _vivoxVoiceManager.OnParticipantAddedEvent += VivoxVoiceManager_OnParticipantAddedEvent;
            _vivoxVoiceManager.JoinChannel(channelName, ChannelType.NonPositional, VivoxVoiceManager.ChatCapability.TextAndAudio);
        }
        else
        {
            Debug.Log("Elsed");
            if (lobbychannel.AudioState == ConnectionState.Disconnected)
            {
                Debug.Log("Elsed2");
                // Ask for hosts since we're already in the channel and part added won't be triggered.
                _vivoxNetworkManager.SendLobbyUpdate(VivoxNetworkManager.MatchStatus.Seeking);

                lobbychannel.BeginSetAudioConnected(true, true, ar =>
                {
                    Debug.Log("Now transmitting into lobby channel");
                });
            }
        }
        // Do nothing, participant added will take care of this
    }

    public void LeaveAllChannels(string channelName, bool includeLobby = true)
    {
        if (_currentChannel == null)
        {
            return;
        }

        _currentChannel = null;
        _vivoxVoiceManager.DisconnectAllChannels();
    }

    private void VivoxVoiceManager_OnParticipantAddedEvent(string username, ChannelId channel, IParticipant participant)
    {
        if (channel.Name == _vivoxNetworkManager.LobbyChannelName && participant.IsSelf)
        {
            // if joined the lobby channel and we're not hosting a match
            // we should request invites from hosts
            _vivoxNetworkManager.SendLobbyUpdate(VivoxNetworkManager.MatchStatus.Seeking);
        }
    }


    void Login()
    {
        Debug.Log("Logging in ");

        try
        {
            _vivoxVoiceManager.Login(UnityEngine.Random.Range(0, 1000).ToString());
        }
        catch (InvalidOperationException e)
        {
            _vivoxVoiceManager.init();
            Invoke("Login", 0.1f);
        }
    }

    void Logout()
    {
        if (_currentChannel != null)
        {
            LeaveAllChannels(_currentChannel, includeLobby: false);
        }

        _vivoxVoiceManager.Logout();
        //_vivoxVoiceManager.OnUserLoggedInEvent -= OnUserLoggedIn;
        //_vivoxVoiceManager.OnUserLoggedOutEvent -= OnUserLoggedOut;
        _vivoxVoiceManager.OnParticipantAddedEvent -= VivoxVoiceManager_OnParticipantAddedEvent;
    }

    //private void OnDestroy()
    //{
    //    Logout();
    //}

    //void OnApplicationQuit()
    //{
    //    Logout();
    //}

    void Update()
    {
        if (!demo)
            return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            JoinLobbyChannel("FirstScene");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            LeaveAllChannels("FirstScene", includeLobby: false);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            JoinLobbyChannel("Zed");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            LeaveAllChannels("Zed", includeLobby: false);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Login();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Logout();
        }
    }
}
