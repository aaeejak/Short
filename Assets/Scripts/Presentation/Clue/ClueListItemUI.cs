using UnityEngine;
using UnityEngine.UI;

public class ClueListItemUI : MonoBehaviour
{
    private Image background;
    private Text nameText;         
    private bool isDiscovered;
    private string originalText; // 원래 텍스트 저장

    private static readonly Color COLOR_HIGHLIGHT = new Color(1f, 0.95f, 0.3f, 0.55f);  // 더 진한 형광펜
    private static readonly Color COLOR_SELECTED_TEXT = new Color(0f, 0f, 0f, 1f);         // 검은색
    private static readonly Color COLOR_NORMAL_TEXT = new Color(0f, 0f, 0f, 1f);           // 검은색
    private static readonly Color COLOR_DIM_TEXT = new Color(0.3f, 0.3f, 0.3f, 1f);        // 진한 회색

    public void Setup(Image bg, Text text, bool discovered)
    {
        this.background = bg;
        this.nameText = text;
        this.isDiscovered = discovered;
        this.originalText = text.text; // 원래 텍스트 저장
    }

    public void SetSelected(bool selected)
    {
        if (selected)
        {
            // 선택됨: ▶ 화살표 + 형광펜 하이라이트
            background.color = COLOR_HIGHLIGHT;
            nameText.fontStyle = FontStyle.Bold;
            nameText.text = "▶" + originalText;
            nameText.color = isDiscovered ? COLOR_SELECTED_TEXT : COLOR_DIM_TEXT;
        }
        else
        {
            // 미선택: 일반 상태
            background.color = Color.clear;
            nameText.fontStyle = FontStyle.Normal;
            nameText.text = originalText;
            nameText.color = isDiscovered ? COLOR_NORMAL_TEXT : COLOR_DIM_TEXT;
        }
    }
}
