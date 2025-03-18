using System.Collections.Generic;
using UnityEngine;

namespace CrazyPawn
{
    public class Socket : MonoBehaviour, IDraggable
    {
        [SerializeField] private Material activeMaterial;
        [SerializeField] private Material defaultMaterial;
    
        private Renderer renderer;
        private static Socket selectedConnector;
        private static List<Socket> allSockets = new List<Socket>();

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            allSockets.Add(this);
        }

        private void OnDestroy()
        {
            allSockets.Remove(this);
        }

        private void OnMouseDown()
        {
            if (selectedConnector == null)
            {
                StartConnection();
            }
            else
            {
                FinishConnection();
            }
        }

        private void StartConnection()
        {
            selectedConnector = this;
            HighlightAvailableConnectors(true);
        }

        private void FinishConnection()
        {
            if (selectedConnector != this && CanConnectTo(selectedConnector))
            {
                GameManager.Instance.ConnectionManager.CreateConnection(selectedConnector, this);
            }
            selectedConnector.HighlightAvailableConnectors(false);
            selectedConnector = null;
        }

        private void HighlightAvailableConnectors(bool highlight)
        {
            foreach (var connector in allSockets)
            {
                if (connector != this && CanConnectTo(connector))
                {
                    connector.renderer.material = highlight ? activeMaterial : defaultMaterial;
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