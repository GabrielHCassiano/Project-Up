using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelect : MonoBehaviour
{
    private EventSystem eventSystem;
    private GameObject lastObj;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eventSystem.currentSelectedGameObject != null)
        {
            lastObj = eventSystem.currentSelectedGameObject;
        }

        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(lastObj);
        }
    }
}
