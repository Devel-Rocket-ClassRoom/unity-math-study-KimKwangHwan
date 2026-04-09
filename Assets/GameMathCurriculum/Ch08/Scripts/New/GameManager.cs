using UnityEngine;

public class GameManager : MonoBehaviour
{
    private IDraggable onDragTarget;
    private Camera cam;
    private bool isDragging;

    private void Awake()
    {
        cam = Camera.main;
        isDragging = false;
        onDragTarget = null;
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Target"))
                {
                    onDragTarget = hit.collider.GetComponent<IDraggable>();
                    if (onDragTarget != null)
                    {
                        isDragging = true;
                        onDragTarget.OnDragStart(hit.point);
                    }
                }
            }
        }

        if (isDragging && onDragTarget != null && Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                onDragTarget.OnDrag(hit.point);
            }
        }

        if (onDragTarget != null && isDragging && Input.GetMouseButtonUp(0))
        {
            onDragTarget.OnDragEnd();
            onDragTarget = null;
            isDragging = false;
        }
    }
}
