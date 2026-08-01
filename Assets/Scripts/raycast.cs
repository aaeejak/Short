using UnityEngine;

[DefaultExecutionOrder(-10)]
public class raycast : MonoBehaviour
{
    public float maxDistance = 1f; // 상호작용 가능 사거리

    public bool HasHit { get; private set; }
    public RaycastHit CurrentHit { get; private set; }

    int layerMask;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        if (ClueBoardManager.IsBoardOpen)
        {
            HasHit = false;
            return;
        }

        // 카메라에서 정면으로 maxDistance까지 레이 발사
        RaycastHit hit;
        HasHit = Physics.Raycast(transform.position, transform.forward, out hit, maxDistance);
        CurrentHit = hit;
    }
}
