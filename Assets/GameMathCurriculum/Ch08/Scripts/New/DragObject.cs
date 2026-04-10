using Unity.VisualScripting;
using UnityEngine;

public class DragObject : MonoBehaviour, IDraggable
{
    private Vector3 startPosition;
    private Vector3 originPosition;
    private bool inDropZone;
    private bool isDragged;
    private bool isDragging;
    private Vector3 draggedVector;
    private Transform dropZone;
    private float timer;
    private float returnDuration = 2f;
    [SerializeField] private Terrain terrain;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Color color;
    private void Awake()
    {
        inDropZone = false;
        isDragged = false;
        isDragging = false;
        GetComponent<Renderer>().material.color = color;
    }
    public void OnDragStart(Vector3 hitPoint)
    {
        originPosition = transform.position;
        draggedVector = transform.position - new Vector3(hitPoint.x, terrain.SampleHeight(transform.position) + GetComponent<Renderer>().bounds.size.y / 2f, hitPoint.z);
        isDragging = true;
    }

    public void OnDrag(Vector3 hitPoint)
    {
        transform.position = new Vector3(hitPoint.x, terrain.SampleHeight(transform.position) + GetComponent<Renderer>().bounds.size.y / 2f, hitPoint.z) + draggedVector;
    }

    public void OnDragEnd()
    {
        isDragging = false;
        if (inDropZone && dropZone != null)
        {
            transform.position = new Vector3(dropZone.position.x, dropZone.position.y + dropZone.GetComponent<Renderer>().bounds.size.y / 2f + GetComponent<Renderer>().bounds.size.y / 2f, dropZone.position.z);
        }
        else
        {
            startPosition = transform.position;
            isDragged = true;
        }
    }

    private void Update()
    {
        if (!inDropZone && isDragged)
        {
            timer += Time.deltaTime / returnDuration;
            transform.position = Vector3.MoveTowards(startPosition, originPosition, timer * moveSpeed);
            if (timer >= 1f)
            {
                transform.position = originPosition;
                timer = 0f;
            }
        }
        if (transform.position == originPosition)
        {
            isDragged = false;
            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDragging && other.CompareTag("DropZone"))
        {
            inDropZone = true;
            dropZone = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isDragging && other.CompareTag("DropZone"))
        {
            inDropZone = false;
            dropZone = null;
        }
    }

}
