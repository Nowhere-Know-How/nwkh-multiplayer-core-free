using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerOnlineItemDetails : MonoBehaviour
{
    public TextMeshProUGUI nameTMPro;
    public TextMeshProUGUI rankTMPro;

    public void SetDetails(string name, int rank)
    {
        nameTMPro.text = name;
        rankTMPro.text = (rank + 1).ToString();
    }
}
