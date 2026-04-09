using UnityEngine;

public class Player_controller : MonoBehaviour
{
    public float speed; // 플레이어 이동속도

    float hAxis;
    float vAxis;
    Vector3 moveVec;

    void Start()
    {

    }

    void Update()
    {
        
        hAxis = Input.GetAxisRaw("Horizontal"); 
        vAxis = Input.GetAxisRaw("Vertical");  

        
        moveVec = (transform.right * hAxis) + (transform.forward * vAxis);
        moveVec = moveVec.normalized; 

        transform.position += moveVec * speed * Time.deltaTime;
    }
}
