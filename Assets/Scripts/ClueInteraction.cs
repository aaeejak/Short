using UnityEngine;

[DefaultExecutionOrder(10)]
public class ClueInteraction : MonoBehaviour
{
    public raycast raycastDetector;
    public ObjectPickup objectPickup;
    public InteractionHighlight highlight;

    [Header("단서 시스템")]
    [Tooltip("ClueBoardManager 컴포넌트가 있는 오브젝트를 드래그")]
    public ClueBoardManager clueBoardManager;

    void Update()
    {
        if (ClueBoardManager.IsBoardOpen) return;

        if (!raycastDetector.HasHit)
        {
            highlight.Hide();
            return;
        }

        // ObjectPickup이 이번 프레임에서 처리했으면 건너뜀
        if (objectPickup.IsHandledThisFrame) return;

        // ── 단서(Clue) 시스템 ──
        ClueObject clueObject = raycastDetector.CurrentHit.collider.GetComponent<ClueObject>();

        if (clueObject != null)
        {
            highlight.Show("조사E");

            // E키로 단서 발견
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (clueBoardManager != null && clueObject.clueData != null)
                {
                    clueBoardManager.DiscoverClue(clueObject.clueData.clueId);
                }
                else
                {
                    Debug.LogWarning("[ClueSystem] ClueBoardManager 또는 ClueData가 연결되지 않았습니다!");
                }
            }
        }
        else
        {
            highlight.Hide();
        }
    }
}
