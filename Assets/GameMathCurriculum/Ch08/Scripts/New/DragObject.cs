using Unity.VisualScripting;
using UnityEngine;

public class DragObject : MonoBehaviour, IDraggable
{
    private Vector3 startPosition;
    private bool inDropZone;
    private bool isDragged;
    private Vector3 draggedVector;
    [SerializeField] private Terrain terrain;
    [SerializeField] private float moveSpeed = 3f;
    private void Awake()
    {
        inDropZone = false;
    }
    public void OnDragStart(Vector3 hitPoint)
    {
        startPosition = transform.position;
        draggedVector = transform.position - new Vector3(hitPoint.x, terrain.SampleHeight(transform.position) + GetComponent<Renderer>().bounds.size.y / 2f, hitPoint.z);
    }

    public void OnDrag(Vector3 hitPoint)
    {
        
    }

    public void OnDragEnd()
    {
        isDragged = true;
    }

    private void Update()
    {
        if (!inDropZone && isDragged)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
        }
        if (transform.position == startPosition)
        {
            isDragged = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropZone"))
        {
            inDropZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DropZone"))
        {
            inDropZone = false;
        }
    }

}
