using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using UnityEngine.Networking;
using MiniJSON;

public class NakamaApi : SingletonBehaviour<NakamaApi>
{
    public string serverIpAddress;
    public int serverPort;

    bool authed = false;
    const string server_key = "ZdsG11p&y13zl6a";
    const string http_key = "XiHe41dci9";


    ISocket matchSocket = null;

    string deviceId;
    Client client;
    ISession session;
    IMatch match;
    string activeSceneMatchId;
    string server_url;

    public ISession Session
    {
        get { return session; }
    }

    public IMatch Match
    {
        get { return match; }
    }

    public ISocket MatchSocket
    {
        get { return matchSocket; }
    }
    public Client Client
    {
        get { return client; }
    }
    void Start()
    {
        deviceId = SystemInfo.deviceUniqueIdentifier;
        client = new Client("http", serverIpAddress, serverPort, server_key);
        ServerDiscovery();
    }

    private async void OnDestroy()
    {

        if (matchSocket != null)
            await matchSocket.LeaveMatchAsync(match);
    }

    public async void JoinMatchIdAsync(string matchId)
    {
        if (matchSocket == null)
        {
            matchSocket = client.NewSocket();
            await matchSocket.ConnectAsync(session);
        }
        activeSceneMatchId = matchId;
        match = await matchSocket.JoinMatchAsync(matchId);
        EventManager.onRoomJoin.Invoke();

        //foreach (var presence in match.Presences)
        //{
        //    Debug.LogFormat("User id '{0}' name '{1}'.", presence.UserId, presence.Username);
        //}
    }

    public async void LeaveMatch()
    {
        await matchSocket.LeaveMatchAsync(activeSceneMatchId);
    }

    public IEnumerator ClientJoinMatchByMatchId(string label)
    {
        //Debug.Log("Joining match");
        string endpoint = server_url + "/v2/rpc/join_match_rpc?http_key=" + http_key;

        var request = new UnityWebRequest(endpoint, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        string dataJsonString = "\"{\\\"modulename\\\": \\\"match\\\",\\\"label\\\": \\\"" + label + "\\\" }\"";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(dataJsonString);
        UploadHandler uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.uploadHandler = uploadHandler;
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer(); 
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.Log("Error" + request.error + ": " + request.downloadHandler.text);
        }
        else
        {
            //Debug.Log("Status Code" + request.responseCode + ": " + request.downloadHandler.text);
            MatchJoinResponse response = JsonUtility.FromJson<MatchJoinResponse>(request.downloadHandler.text);
            EventManager.onGetMatchId.Invoke(response);
        }
    }

    #region MainMenu
    void ServerDiscovery()
    {
        StartCoroutine(PingServer());
    }

    IEnumerator PingServer()
    {
        yield return new WaitForSeconds(0.5f);
        server_url = "http://" + serverIpAddress + ":" + serverPort.ToString();
        Debug.Log(server_url);
        UnityWebRequest resp = UnityWebRequest.Get(server_url);
        yield return resp.SendWebRequest();

        if (resp.isNetworkError || resp.isHttpError)
        {
            Debug.Log("Server seems offline. Please close and try again.");
            //throw new System.EntryPointNotFoundException(resp.error);
        }
        else
        {
            EventManager.onServerDiscovery.Invoke();
        }
    }

    public async void Register(string name, string email, string password, PlayerGender gender)
    {
        try
        {
            session = await client.AuthenticateEmailAsync(email, password, username: name);
            SaveGenderSelection(gender);
            DebugInfo.SetToast("Registration Success", "Your account has been created. Please log in with your character name.");
            EventManager.onAccountCreation.Invoke(AccountRegisterResolution.SUCCESS);
        }
        catch (ApiResponseException e)
        {
            Debug.Log(e.Message);
            if (e.Message == "Username is already in use.")
            {
                DebugInfo.SetToast("Error", e.Message);
                EventManager.onAccountCreation.Invoke(AccountRegisterResolution.FAILED);
            }
            else if (e.Message == "Invalid credentials.")
            {
                DebugInfo.SetToast("Error", "Email already in use.");
                EventManager.onAccountCreation.Invoke(AccountRegisterResolution.FAILED);
            }
            else
            {
                DebugInfo.SetToast("Error", e.Message);
                EventManager.onAccountCreation.Invoke(AccountRegisterResolution.FAILED);
            }
        }
    }

    public async void Login(string account, string password)
    {
        try
        {
            session = await client.AuthenticateEmailAsync(account, password, username: name, create: false);
            authed = true;

            DebugInfo.SetToast("Login Success", "Entering the world!");
            EventManager.onLoginAttempt.Invoke(AccountLoginResolution.SUCCESS);
        }
        catch (ApiResponseException e)
        {
            DebugInfo.SetToast("Error", e.Message);
            EventManager.onLoginAttempt.Invoke(AccountLoginResolution.FAILED);
        }
    }

    void SaveGenderSelection(PlayerGender gender)
    {
        WriteStorageObject[] obj = new WriteStorageObject[]
        {
            new WriteStorageObject{
                Collection = "character",
                Key = "base",
                Value = "{\"Gender\":\"" + gender.ToString() + "\"}"
            }
        };
        StoreData(obj);
    }
    #endregion

    public async void GetLoginInfo()
    {
        StorageObjectId[] objs = new StorageObjectId[]
        {
            new StorageObjectId
            {
                Collection = "character",
                Key = "location",
                UserId = session.UserId
            }
        };
        IApiStorageObjects result = await client.ReadStorageObjectsAsync(session, objs);
        foreach (IApiStorageObject entry in result.Objects)
        {
            //Debug.Log(entry.Value);
            PlayerDataResponse pData = JsonUtility.FromJson<PlayerDataResponse>(entry.Value);
            EventManager.onGetLoginInformation.Invoke(pData);
            return;
        }

        EventManager.onGetLoginInformation.Invoke(new PlayerDataResponse());

    }


    public async void StoreData(WriteStorageObject[] objects)
    {
        var objectIds = await client.WriteStorageObjectsAsync(session, objects);
        Debug.LogFormat("Successfully stored objects: [{0}]", string.Join(",\n   ", objectIds));
    }

    public async void GetData(StorageObjectId[] objectIds)
    {
        var result = await client.ReadStorageObjectsAsync(session, objectIds);
        Debug.LogFormat("Read objects: [{0}]", string.Join(",\n  ", result.Objects));
    }
}
