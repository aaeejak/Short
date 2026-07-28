using UnityEngine;

public class Player_controller : MonoBehaviour
{
    public float speed; // 플레이어 이동속도
    public float jumpForce = 5f; // 점프 힘
    public float groundCheckDistance = 0.3f; // 바닥 체크 거리
    public LayerMask groundLayer; // 바닥 레이어

    float hAxis;
    float vAxis;
    Vector3 moveVec;

    Rigidbody rb;
    bool isGrounded;
    bool jumpRequested;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 단서 보드가 열려있으면 이동 차단
        if (ClueBoardManager.IsBoardOpen) return;

        hAxis = Input.GetAxisRaw("Horizontal"); 
        vAxis = Input.GetAxisRaw("Vertical");  

        // 착지 상태에서 Space 누르면 점프 예약
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            jumpRequested = true;
    }

    void FixedUpdate()
    {
        // 착지 판정
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // 이동
        moveVec = (transform.right * hAxis + transform.forward * vAxis).normalized;
        rb.MovePosition(rb.position + moveVec * speed * Time.fixedDeltaTime);

        // 점프
        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }
}
