using UnityEngine;

public interface IDraggable
{
    void OnDragStart(Vector3 hitPoint);
    void OnDragEnd();
    void OnDrag(Vector3 hitPoint);
}
