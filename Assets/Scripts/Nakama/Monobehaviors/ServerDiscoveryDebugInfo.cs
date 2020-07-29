using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServerDiscoveryDebugInfo : MonoBehaviour
{
    public TextMeshProUGUI text;
    public NakamaApi nakamaApi;

    private void Start()
    {
        text.text = "Server discovery... " + nakamaApi.serverIpAddress + ":" + nakamaApi.serverPort.ToString();
    }
}
