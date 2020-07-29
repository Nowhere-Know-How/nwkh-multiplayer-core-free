using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputFieldTabCycle : MonoBehaviour
{
    EventSystem system;
    void Start()
    {
        system = EventSystem.current;
    }

    public void Update()
    {
        if (system.currentSelectedGameObject)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                if (next != null)
                {
                    InputField inputfield = next.GetComponent<InputField>();
                    if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(system));
                    system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
                }
            }
        }
    }
}
