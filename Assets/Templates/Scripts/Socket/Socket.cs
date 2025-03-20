using CrazyPawn;
using UnityEngine;

namespace Templates.Scripts.Socket
{
    public class Socket : MonoBehaviour, IDraggable
    {
        [SerializeField] private Material activeMaterial;
        [SerializeField] private Material defaultMaterial;
        public Pawn.Pawn ParentPawn => parentPawn;
        
        private SocketManager socketManager;
        private Renderer renderer;
        private Pawn.Pawn parentPawn;
        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            parentPawn = GetComponentInParent<Pawn.Pawn>();
            socketManager = GameManager.Instance.SocketManager;
            
            parentPawn.AddSocket(this);
        }

        public void ChangeActivateMaterial(bool isActive = true)
        {
            renderer.material = isActive ? activeMaterial : defaultMaterial;
        }
        
        public void OnDragStart()
        {
            socketManager.HighlightAvailableConnectors(this, true);
        }

        public void OnDragEnd()
        {
            socketManager.HighlightAvailableConnectors(this, false);
        }

        public void OnDrag(Vector3 position)
        {
        }
    }
}