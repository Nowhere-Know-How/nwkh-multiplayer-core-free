using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static OnServerDiscovery             onServerDiscovery               = new OnServerDiscovery();
    public static OnAccountCreation             onAccountCreation               = new OnAccountCreation();
    public static OnLoginAttempt                onLoginAttempt                  = new OnLoginAttempt();
    public static OnGetMatchId                  onGetMatchId                    = new OnGetMatchId();
    public static OnRoomJoin                    onRoomJoin                      = new OnRoomJoin();
    public static OnRemoteConnectedPlayer       onRemoteConnectedPlayer         = new OnRemoteConnectedPlayer();
    public static OnRemoteDisconnectedPlayer    onRemoteDisconnectedPlayer      = new OnRemoteDisconnectedPlayer();
    public static OnLocalConnectedPlayer        onLocalConnectedPlayer          = new OnLocalConnectedPlayer();
    public static OnGetLoginInformation         onGetLoginInformation           = new OnGetLoginInformation();

    private EventManager() { }

    public static IEnumerator DelayInvoke(float delay, UnityEvent e)
    {
        yield return new WaitForSeconds(delay);
        e.Invoke();
    }

    public static IEnumerator DelayInvoke(float delay, OnAccountCreation e, AccountRegisterResolution resolution)
    {
        yield return new WaitForSeconds(delay);
        e.Invoke(resolution);
    }
}
