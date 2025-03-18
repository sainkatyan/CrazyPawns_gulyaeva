using UnityEngine;

namespace CrazyPawn
{
    public interface IDraggable
    {
        void OnDragStart();
        void OnDrag(Vector3 position);
        void OnDragEnd();
    }
}