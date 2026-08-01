using UnityEngine;
using TMPro;

public class InteractionHighlight : MonoBehaviour
{
    public Renderer targetObject_wire; // 외곽선 쉐이더
    public GameObject text; // 상호작용 문구

    TMP_Text tmpText;

    void Awake()
    {
        if (text != null)
            tmpText = text.GetComponent<TMP_Text>();
    }

    public void Show(string message)
    {
        if (targetObject_wire != null)
            targetObject_wire.enabled = true;

        if (tmpText != null)
        {
            tmpText.text = message;
            text.SetActive(true);
        }
    }

    public void Hide()
    {
        if (targetObject_wire != null)
            targetObject_wire.enabled = false;

        if (text != null)
            text.SetActive(false);
    }
}
