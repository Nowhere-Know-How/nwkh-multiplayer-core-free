using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGate : MonoBehaviour
{
    public string nextScene;
    public Transform nextTransform;

    NakamaApi nakama;

    private void Start()
    {
        nakama = FindObjectOfType<NakamaApi>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "local_player")
        {
            WarpGateData.SetTransform(nextTransform);
            WarpGateData.nextSceneName = nextScene;
            nakama.LeaveMatch();
            StartCoroutine(nakama.ClientJoinMatchByMatchId(nextScene));
        }
    }
}
