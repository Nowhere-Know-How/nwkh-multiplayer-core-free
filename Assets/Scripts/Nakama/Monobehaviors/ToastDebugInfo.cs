using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToastDebugInfo : MonoBehaviour
{
    public TMP_Text header;
    public TMP_Text message;

    private void Start()
    {
        header.text = DebugInfo.Header;
        message.text = DebugInfo.Message;
    }
}
