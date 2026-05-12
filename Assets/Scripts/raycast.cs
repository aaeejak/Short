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


    void Update()
    {
        // 레이캐스트 충돌 정보
        RaycastHit hit;

        // 카메라에서 정면으로 maxDistance까지 켄버스를 무시하고 레이 발사
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            // 레이케스트가 Cube에 닿은 경우
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
                }

                // 외각선 쉐이더 켜기 & 집기
                if (targetObject_wire != null)
                {
                    targetObject_wire.enabled = true;
                    text.GetComponent<TMP_Text>().text = "집기E";
                    text.SetActive(true);
                }
            }


            else if (hit.collider.gameObject.name == "Space_test_001" && istaking)
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
                // 외각선 쉐이더 끄기
                if (targetObject_wire != null)
                {
                    targetObject_wire.enabled = false;
                    text.SetActive(false);
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



        if (Input.GetMouseButtonDown(0) && istaking)
        {
            if (takingobj == "Cube")
            {
                Debug.Log("Cube");
            }
        }

    }
}
