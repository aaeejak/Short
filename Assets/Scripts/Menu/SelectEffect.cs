using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectEffect : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler
{
    public GameObject effectObject;

    void Awake()
    {
        if (effectObject != null)
        {
            effectObject.SetActive(false);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (effectObject != null)
        {
            effectObject.SetActive(true);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (effectObject != null)
        {
            effectObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}