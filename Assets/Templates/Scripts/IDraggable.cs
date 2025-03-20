using UnityEngine;

namespace Templates.Scripts
{
    public interface IDraggable
    {
        void OnDragStart();
        void OnDrag(Vector3 position);
        void OnDragEnd();
    }
}