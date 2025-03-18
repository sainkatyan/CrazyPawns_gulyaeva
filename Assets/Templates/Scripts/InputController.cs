using UnityEngine;

namespace CrazyPawn
{
    public class InputController : MonoBehaviour
    {
        private Camera camera;
        private IDraggable currentDraggable;
        [SerializeField] private Transform chessboard;

        private void Start()
        {
            camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryStartDrag();
            }

            if (Input.GetMouseButton(0) && currentDraggable != null)
            {
                Drag();
            }

            if (Input.GetMouseButtonUp(0) && currentDraggable != null) // ЛКМ отпущена
            {
                currentDraggable.OnDragEnd();
                currentDraggable = null;
            }
        }

        private void TryStartDrag()
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("PawnBody"))
                {
                    IDraggable draggable = hit.collider.GetComponentInParent<IDraggable>();
                    currentDraggable = draggable;
                    currentDraggable.OnDragStart();
                }
            }
        }

        private void Drag()
        {
            if (currentDraggable == null) return;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform == chessboard.transform)
                {
                    Vector3 newPosition = hit.point;
                    currentDraggable.OnDrag(new Vector3(newPosition.x, 0f, newPosition.z));
                }
            }
        }
    }
}