using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VivoxUnity;
using System.Linq;

public class VivoxControls : MonoBehaviour
{
    public bool demo = false;

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
    }

    public void JoinLobbyChannel(string channelName)
    {
        if (_currentChannel != null)
        {
            return;
        }

        _currentChannel = channelName;
        var lobbychannel = _vivoxVoiceManager.ActiveChannels.FirstOrDefault(ac => ac.Channel.Name == _vivoxNetworkManager.LobbyChannelName);
        if ((_vivoxVoiceManager && _vivoxVoiceManager.ActiveChannels.Count == 0)
            || lobbychannel == null)
        {
            _vivoxVoiceManager.OnParticipantAddedEvent += VivoxVoiceManager_OnParticipantAddedEvent;
            _vivoxVoiceManager.JoinChannel(channelName, ChannelType.NonPositional, VivoxVoiceManager.ChatCapability.TextAndAudio);
        }
        else
        {
            if (lobbychannel.AudioState == ConnectionState.Disconnected)
            {
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
        _vivoxVoiceManager.Login(Random.RandomRange(0, 1000).ToString());
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
            JoinLobbyChannel("Alpha");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            LeaveAllChannels("Alpha", includeLobby: false);
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
