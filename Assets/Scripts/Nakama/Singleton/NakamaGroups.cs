using Nakama;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NakamaGroups : SingletonBehaviour<NakamaGroups>
{
    const string online_players_group_id = "61bad96f-a5f7-4298-b057-88eb2c3780c2";

    NakamaApi nakama;
    ISession session;
    Client client;

    List<OnlinePlayerInfo> playersOnline = new List<OnlinePlayerInfo>();

    public List<OnlinePlayerInfo> PlayersOnline
    {
        get { return playersOnline; }
    }

    void Start()
    {
        nakama = FindObjectOfType<NakamaApi>();
        session = nakama.Session;
        client = nakama.Client;
        JoinGroup(online_players_group_id); //players online group
        GetOnlinePlayersList();
    }

    public void PollPlayerData()
    {
        InvokeRepeating("GetOnlinePlayersList", 0f, 2f);
    }

    public void StopPollingPlayerData()
    {
        CancelInvoke();
    }

    public void GetOnlinePlayersList()
    {
        ListGroupMembers(online_players_group_id);
    }

    async void ListGroupMembers(string groupId)
    {
        var result = await client.ListGroupUsersAsync(session, groupId, state:2, limit:100, cursor:null);

        playersOnline.Clear();
        
        foreach (var ug in result.GroupUsers)
        {
            IApiUser g = ug.User;
            if (g.Online)
                playersOnline.Add(new OnlinePlayerInfo(g.Username, g.Id));
            //Debug.LogFormat("User '{0}' role '{1}', '{2}'", g.Id, g.Username, g.Online);
        }

        if (playersOnline.Count == 0)
        {
            ListGroupMembers(online_players_group_id);
        }
    }

    async void JoinGroup(string groupId)
    {
        await client.JoinGroupAsync(session, groupId);
        //Debug.LogFormat("Sent group join request '{0}'", groupId);
    }
    async void LeaveGroup(string groupId)
    {
        await client.LeaveGroupAsync(session, groupId);
        //Debug.LogFormat("Sent group leave request '{0}'", groupId);
    }
    void OnDestroy()
    {
        if (session != null)
            LeaveGroup(online_players_group_id); //players online group
    }

}
