////using Gamevanilla;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MakiPopupOpener : PopupOpener
//{
//    public override void OpenPopup()
//    {
//        if (m_canvas == null)
//        {
//            m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
//        }

//        m_popup = Instantiate(popupPrefab, m_canvas.transform, false);
//        m_popup.SetActive(true);
//        m_popup.transform.localScale = Vector3.zero;
//        //m_popup.GetComponent<Popup>().Open();
//    }

//    public void ClosePopup()
//    {
//        GameObject.Destroy(m_popup);
//    }
//}
