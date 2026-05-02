using UnityEngine;

public class raycast : MonoBehaviour
{
    public float maxDistance = 1f; // 상호작용 가능 사거리
    public Renderer targetObject_wire; // 외곽선 쉐이더
    public GameObject text; // 상호작용 문구
    int layerMask = LayerMask.GetMask("Interactable"); // 켄버스를 무시하도록 설정

    void Update()
    {
        // 레이캐스트 충돌 정보
        RaycastHit hit;


        // 카메라에서 정면으로 maxDistance까지 켄버스를 무시하고 레이 발사
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            // 레이케스트가 Cube에 닿은 경우
            if (hit.collider.gameObject.name == "Cube")
            {
                // 상호작용
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("hello world!");
                }

                // 외각선 쉐이더 켜기
                if (targetObject_wire != null)
                {
                    targetObject_wire.enabled = true;
                    text.SetActive(true);
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
}
