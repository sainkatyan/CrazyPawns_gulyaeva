using UnityEngine;

namespace CrazyPawn
{
    public class InputController : MonoBehaviour
    {
        private Camera camera;
        private IDraggable currentDraggablePawn;
        private Socket draggingSocket;
        [SerializeField] private Transform chessboard;
        private float timer;
        private bool isSecondClick = false;
        private GameManager gameManager;

        private void Start()
        {
            camera = Camera.main;
            gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                timer = Time.time;
                
                
                    TryStartDrag();
                
            }

            if (Input.GetMouseButton(0) && currentDraggablePawn != null)
            {
                Drag();
            }

            if (Input.GetMouseButtonUp(0)) 
            {
                if (Time.time - timer < 0.5f)
                {
                    if (isSecondClick == false)
                    {
                        isSecondClick = true;
                        
                        Debug.Log("Start");
                        TryStartDrag();
                    }
                    else
                    {
                        isSecondClick = false;
                        if (draggingSocket != null)
                        {
                            Debug.Log("End");

                            if (draggingSocket != null)
                            {
                                FinishDrag();
                            }
                        }
                        
                    }
                    return;
                }

                if (currentDraggablePawn != null)
                {
                    currentDraggablePawn.OnDragEnd();
                    currentDraggablePawn = null;
                }

                if (draggingSocket != null)
                {
                    FinishDrag();
                }
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
                    currentDraggablePawn = draggable;
                    currentDraggablePawn.OnDragStart();
                }

                if (isSecondClick == false)
                {
                    draggingSocket = hit.collider.GetComponent<Socket>();
                    if (draggingSocket != null)
                    {
                        draggingSocket.OnDragStart();
                    }
                }
            }
        }

        private void Drag()
        {
            if (currentDraggablePawn == null) return;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform == chessboard.transform)
                {
                    Vector3 newPosition = hit.point;
                    currentDraggablePawn.OnDrag(new Vector3(newPosition.x, 0f, newPosition.z));
                }
            }
        }

        private void FinishDrag()
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Socket targetSocket = hit.collider.GetComponent<Socket>();
                if (targetSocket == null)
                {
                    gameManager.SocketManager.HighlightAvailableConnectors(draggingSocket,false);
                    draggingSocket = null;
                    
                    return;
                }

                if (CanConnect(targetSocket))
                {
                    gameManager.ConnectionManager.CreateConnection(draggingSocket, targetSocket);
                }
            }

            if (draggingSocket != null)
            {
                draggingSocket.OnDragEnd();
                draggingSocket = null;
            }
        }

        private bool CanConnect( Socket targetSocket)
        {
            return targetSocket != null && draggingSocket != targetSocket 
                                        && gameManager.SocketManager.CanConnect(draggingSocket, targetSocket);
        }
    }
}