using System;
using Templates.Scripts;
using Templates.Scripts.Socket;
using Unity.VisualScripting;
using UnityEngine;

namespace CrazyPawn
{
    public class ConnectionView : MonoBehaviour
    {
        private Socket connectorA;
        private Socket connectorB;
        private LineRenderer lineRenderer;
        private const float WIDTH = 0.07f;
        public void Initialize(Socket a, Socket b)
        {
            connectorA = a;
            connectorB = b;
            lineRenderer = GetComponent<LineRenderer>();

            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = WIDTH;
            lineRenderer.endWidth = WIDTH;
            lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = Color.white };

            UpdateLineConnection();
            SubscriveEvents();
        }

        private void SubscriveEvents()
        {
            connectorA.ParentPawn.OnDestroyed += DestroyConnection;
            connectorB.ParentPawn.OnDestroyed += DestroyConnection;
            
            connectorA.ParentPawn.OnPositionChanged += UpdateLineConnection;
            connectorB.ParentPawn.OnPositionChanged += UpdateLineConnection;
        }
        
        private void UnSubscriveEvents()
        {
            connectorA.ParentPawn.OnDestroyed -= DestroyConnection;
            connectorB.ParentPawn.OnDestroyed -= DestroyConnection;
            
            connectorA.ParentPawn.OnPositionChanged -= UpdateLineConnection;
            connectorB.ParentPawn.OnPositionChanged -= UpdateLineConnection;
        }
        private void UpdateLineConnection()
        {
            if (connectorA == null || connectorB == null) return;
            lineRenderer.SetPosition(0, connectorA.transform.position);
            lineRenderer.SetPosition(1, connectorB.transform.position);
        }

        private void DestroyConnection()
        {
            UnSubscriveEvents();
            GameManager.Instance.ConnectionManager.RemoveConnection(this); 
        }
    }
}