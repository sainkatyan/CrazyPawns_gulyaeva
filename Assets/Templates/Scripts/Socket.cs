using System.Collections.Generic;
using UnityEngine;

namespace CrazyPawn
{
    public class Socket : MonoBehaviour, IDraggable
    {
        [SerializeField] private Material activeMaterial;
        [SerializeField] private Material defaultMaterial;
        public Pawn ParentPawn => parentPawn;
        
        private SocketManager socketManager;
        private Renderer renderer;
        private Pawn parentPawn;
        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            parentPawn = GetComponentInParent<Pawn>();
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