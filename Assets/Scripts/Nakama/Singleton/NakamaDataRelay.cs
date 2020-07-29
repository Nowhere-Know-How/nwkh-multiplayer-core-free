using Nakama;
using Nakama.TinyJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;

//This class is intended to exist only once per scene. It manages the remote game objects and transmits/receives data from the nakama server
public class NakamaDataRelay : MonoBehaviour
{
    public bool debugPackets;

    List<IUserPresence> connectedOpponents = new List<IUserPresence>(2);

    NakamaApi nakama;
    IMatch match;
    ISocket socket;
    ISession session;
    string userId;
    Action<Nakama.IMatchPresenceEvent> matchPresenceHandler;
    Action<Nakama.IMatchState> matchStateHandler;

    Dictionary<string, PlayerDataResponse> playerData = new Dictionary<string, PlayerDataResponse>();

    private void Start()
    {
        nakama = FindObjectOfType<NakamaApi>();
        socket = nakama.MatchSocket;
        match = nakama.Match;
        session = nakama.Session;
        userId = session.UserId;
        Debug.Log("My Client ID is " + userId);

        EventManager.onLocalConnectedPlayer.Invoke();

        SetupHandlers();

        InvokeReceivedMatchPresenceEvent();
        InvokeReceivedMatchStateEvent();

    }

    private void SetupHandlers()
    {
        matchPresenceHandler = (presenceEvent) =>
        {
            foreach (var presence in presenceEvent.Leaves)
            {
                EventManager.onRemoteDisconnectedPlayer.Invoke(presence);
                connectedOpponents.Remove(presence);
            }

            foreach (var presence in presenceEvent.Joins)
            {
                EventManager.onRemoteConnectedPlayer.Invoke(presence);
            }

            connectedOpponents.AddRange(presenceEvent.Joins);
            Debug.LogFormat("Connected opponents: [{0}]", string.Join(",\n  ", connectedOpponents));
        };

        matchStateHandler = (state) =>
        {
            switch (state.OpCode)
            {
                case 1:
                    string data_json_string = System.Text.Encoding.UTF8.GetString(state.State, 0, state.State.Length);
                    if (debugPackets)
                        Debug.Log(data_json_string);

                    Dictionary<string, object> player_data = (Dictionary<string, object>)Json.Deserialize(data_json_string);
                    foreach (KeyValuePair<string, object> entry in player_data)
                    {
                        Dictionary<string, object> client = (Dictionary<string, object>)entry.Value;
                        object data = (Dictionary<string, object>)client["data"];
                        PlayerDataResponse pData = JsonUtility.FromJson<PlayerDataResponse>(Json.Serialize(data)); //TODO: I'm doing an extra serial/deserialize to just get it working. Fix later.
                        pData.name = (string)client["username"];
                        pData.userId = (string)client["user_id"];
                        playerData[pData.userId] = pData;
                    }
                    break;


                default:
                    break;
            }

        };
    }

    private void OnDestroy()
    {
        socket.ReceivedMatchPresence -= matchPresenceHandler;
        socket.ReceivedMatchState -= matchStateHandler;

        connectedOpponents.Clear();
        playerData.Clear();
    }

    public void SendData(GameObject obj)
    {
        long opCode = 1;
        string payload = JsonUtility.ToJson(new PlayerDataRequest(obj));
        string newState = new Dictionary<string, string> { { "payload", payload } }.ToJson();
        socket.SendMatchStateAsync(match.Id, opCode, newState);
    }

    void InvokeReceivedMatchPresenceEvent()
    {
        socket.ReceivedMatchPresence += matchPresenceHandler;

        //Remote players who are already logged in
        connectedOpponents.AddRange(match.Presences);
        //Debug.LogFormat("Connected opponents: [{0}]", string.Join(",\n  ", connectedOpponents));
        for (int i = 0; i < connectedOpponents.Count; i++)
        {
            Debug.Log("Event Emitted");
            EventManager.onRemoteConnectedPlayer.Invoke(connectedOpponents[i]);
        }
    }


    void InvokeReceivedMatchStateEvent()
    {
        socket.ReceivedMatchState += matchStateHandler;
    }

    public Dictionary<string, PlayerDataResponse> PlayerData
    {
        get { return playerData; }
    }

    public string ClientId
    {
        get { return userId; }
    }

    public ISession Session
    {
        get { return session; }
    }
}
