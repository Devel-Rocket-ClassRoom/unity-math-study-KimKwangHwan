using UnityEngine;

public class ControllableObject : MonoBehaviour
{
    private Vector3 destination;
    private bool dropFailed = false;
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private Color color;
    private void Awake()
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }
    public void GoOriginPosition(Vector3 position)
    {
        destination = position;
        dropFailed = true;
    }

    private void Update()
    {
        if (dropFailed)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed);
        }

        if (transform.position == destination)
        {
            dropFailed = false;
        }
    }
}
