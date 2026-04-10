using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    private float speed = 8f;
    private float rotateSpeed = 120f;
    private Vector3 moveDirection;
    private float rotateAngle;
    private Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveDirection * speed * Time.fixedDeltaTime);
        rb.MoveRotation(transform.rotation * Quaternion.AngleAxis(rotateAngle * Time.fixedDeltaTime, transform.up));
        
    }

    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float r = Input.GetAxis("Rotation");

        moveDirection = h * transform.right + v * transform.forward;
        moveDirection.Normalize();
        rotateAngle = r * rotateSpeed;

        //transform.position = transform.position + direction * speed * Time.deltaTime;
        //transform.Rotate(0f, r * rotateSpeed * Time.deltaTime, 0f);
    }
}
