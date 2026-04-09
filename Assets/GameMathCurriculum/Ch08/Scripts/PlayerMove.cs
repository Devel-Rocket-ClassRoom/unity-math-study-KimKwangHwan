using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 8f;
    private float rotateSpeed = 120f;

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (h != 0f || v != 0f)
        {
            Vector3 direction = h * transform.right + v * transform.forward;
            direction.Normalize();

            transform.position = transform.position + direction * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, -rotateSpeed * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
        }
    }
}
