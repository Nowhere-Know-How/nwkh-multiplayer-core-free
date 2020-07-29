using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContentPlayersOnline : MonoBehaviour
{
    public GameObject itemPrefab;

    public List<GameObject> items = new List<GameObject>();

    NakamaGroups nakamaGroups;
    private void Start()
    {
        nakamaGroups = FindObjectOfType<NakamaGroups>();
        nakamaGroups.PollPlayerData();

        

        InvokeRepeating("UpdateItemContent", 0f, 1f);
    }


    private void UpdateItemContent()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < nakamaGroups.PlayersOnline.Count; i++)
        {
            GameObject item = Instantiate(itemPrefab, transform);
            PlayerOnlineItemDetails details = item.GetComponent<PlayerOnlineItemDetails>();
            details.SetDetails(nakamaGroups.PlayersOnline[i].userName, i);
        }
    }

    private void OnDestroy()
    {
        nakamaGroups.StopPollingPlayerData();
    }





}
