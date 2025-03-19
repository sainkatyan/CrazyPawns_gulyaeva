using System.Collections.Generic;
using UnityEngine;

namespace CrazyPawn
{
    public class Socket : MonoBehaviour, IDraggable
    {
        [SerializeField] private Material activeMaterial;
        [SerializeField] private Material defaultMaterial;
    
        private Renderer renderer;
        private static Socket selectedSocket;
        private static List<Socket> allSockets = new List<Socket>();


        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            allSockets.Add(this);
        }

        public Pawn GetPawn()
        {
            return GetComponentInParent<Pawn>();
        }

        private void OnDestroy()
        {
            allSockets.Remove(this);
        }

        private void OnMouseDown()
        {
            /*if (selectedSocket == null)
            {
                Debug.Log("OnMouseDown");
                StartConnection();
            }
            else
            {
                Debug.Log("OnMouseDown");
                FinishConnection();
            }*/
        }

        private void StartConnection()
        {
            selectedSocket = this;
            HighlightAvailableConnectors(true);
        }

        private void FinishConnection()
        {
            if (selectedSocket != this && CanConnectTo(selectedSocket))
            {
                GameManager.Instance.ConnectionManager.CreateConnection(selectedSocket, this);
            }
            selectedSocket.HighlightAvailableConnectors(false);
            selectedSocket = null;
        }

        private void HighlightAvailableConnectors(bool highlight)
        {
            foreach (var socket in allSockets)
            {
                if (socket != this && CanConnectTo(socket))
                {
                    socket.renderer.material = highlight ? activeMaterial : defaultMaterial;
                }
            }
        }

        public bool CanConnectTo(Socket other)
        {
            return transform.parent != other.transform.parent;
        }
        
        public void OnDragStart()
        {
            HighlightAvailableConnectors(true);
        }

        public void OnDragEnd()
        {
            
            HighlightAvailableConnectors(false);
        }

        public void OnDrag(Vector3 position)
        {
        }
    }
}