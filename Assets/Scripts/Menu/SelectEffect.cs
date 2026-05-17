using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectEffect: MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject effectObject;

    public void OnSelect(BaseEventData eventData)
    {
        effectObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        effectObject.SetActive(false);
    }
}