using UnityEngine;

[DefaultExecutionOrder(0)]
public class ObjectPickup : MonoBehaviour
{
    public raycast raycastDetector;
    public InteractionHighlight highlight;
    public GameObject player;
    public GameObject interaction_object;

    [Header("단서 시스템")]
    [Tooltip("ClueBoardManager 컴포넌트가 있는 오브젝트를 드래그")]
    public ClueBoardManager clueBoardManager;

    /// <summary>이번 프레임에서 ObjectPickup이 상호작용을 처리했는지 여부</summary>
    public bool IsHandledThisFrame { get; private set; }

    bool istaking = false;
    string takingobj;

    void Update()
    {
        IsHandledThisFrame = false;

        if (ClueBoardManager.IsBoardOpen) return;

        if (raycastDetector.HasHit)
        {
            var hitObj = raycastDetector.CurrentHit.collider.gameObject;

            // ── Cube 집기 (ClueObject보다 먼저 체크) ──
            if (hitObj.name == "Cube" && !istaking)
            {
                IsHandledThisFrame = true;
                highlight.Show("집기E");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    istaking = true;
                    takingobj = "Cube";
                    interaction_object.transform.SetParent(player.transform, true);
                    interaction_object.GetComponent<Collider>().enabled = false;
                    interaction_object.transform.localPosition = new Vector3(2f, -0.5f, 2f);
                    interaction_object.transform.localRotation = Quaternion.identity;

                    // 집을 때 단서 발견 처리
                    ClueObject clueObject = interaction_object.GetComponent<ClueObject>();
                    if (clueObject != null && clueBoardManager != null && clueObject.clueData != null)
                    {
                        clueBoardManager.DiscoverClue(clueObject.clueData.clueId);
                    }
                }
            }
            // ── 내려놓기 ──
            else if (IsOrChildOf(hitObj, "Place_002") && istaking)
            {
                IsHandledThisFrame = true;
                highlight.Show("내려놓기E");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    var hit = raycastDetector.CurrentHit;
                    interaction_object.transform.SetParent(null);
                    interaction_object.transform.position = new Vector3(hit.point.x, hit.point.y + 0.25f, hit.point.z);
                    interaction_object.transform.rotation = Quaternion.identity;
                    interaction_object.GetComponent<Collider>().enabled = true;
                    istaking = false;
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && istaking)
        {
            if (takingobj == "Cube")
            {
                Debug.Log("Cube");
            }
        }
    }

    // 해당 오브젝트 또는 부모 중 하나의 이름이 targetName인지 확인
    bool IsOrChildOf(GameObject obj, string targetName)
    {
        Transform current = obj.transform;
        while (current != null)
        {
            if (current.gameObject.name == targetName)
                return true;
            current = current.parent;
        }
        return false;
    }
}
