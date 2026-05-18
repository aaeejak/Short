using UnityEngine;
using TMPro;

public class raycast : MonoBehaviour
{
    public float maxDistance = 1f; // 상호작용 가능 사거리
    public GameObject player;
    public GameObject interaction_object;
    public Renderer targetObject_wire; // 외곽선 쉐이더
    public GameObject text; // 상호작용 문구
    bool istaking = false;
    string takingobj;

    [Header("단서 시스템")]
    [Tooltip("ClueBoardManager 컴포넌트가 있는 오브젝트를 드래그")]
    public ClueBoardManager clueBoardManager;

    int layerMask;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        if (ClueBoardManager.IsBoardOpen) return;

        // 레이캐스트 충돌 정보
        RaycastHit hit;

        // 카메라에서 정면으로 maxDistance까지 켄버스를 무시하고 레이 발사
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            // ── Cube 집기/내려놓기 (ClueObject보다 먼저 체크) ──
            if (hit.collider.gameObject.name == "Cube" && !istaking)
            {
                // 상호작용
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

                // 외각선 쉐이더 켜기 & 집기
                if (targetObject_wire != null)
                {
                    targetObject_wire.enabled = true;
                    text.GetComponent<TMP_Text>().text = "집기E";
                    text.SetActive(true);
                }
            }
            else if (IsOrChildOf(hit.collider.gameObject, "Place_002") && istaking)
            {
                text.GetComponent<TMP_Text>().text = "내려놓기E";
                text.SetActive(true);

                // 내려놓기
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interaction_object.transform.SetParent(null);
                    interaction_object.transform.position = new Vector3(hit.point.x, hit.point.y + 0.25f, hit.point.z);
                    interaction_object.transform.rotation = Quaternion.identity;
                    interaction_object.GetComponent<Collider>().enabled = true;
                    istaking = false;
                }
            }
            else
            {
                // ── 단서(Clue) 시스템 ──
                ClueObject clueObject = hit.collider.GetComponent<ClueObject>();

                if (clueObject != null)
                {
                    // 외곽선 표시
                    if (targetObject_wire != null)
                    {
                        targetObject_wire.enabled = true;
                        text.GetComponent<TMP_Text>().text = "조사E";
                        text.SetActive(true);
                    }

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
                    // 외각선 쉐이더 끄기
                    if (targetObject_wire != null)
                    {
                        targetObject_wire.enabled = false;
                        text.SetActive(false);
                    }
                }
            }
        }
        else
        {
            if (targetObject_wire != null)
            {
                targetObject_wire.enabled = false;
                text.SetActive(false);
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
