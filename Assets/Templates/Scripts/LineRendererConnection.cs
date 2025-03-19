using System;
using Unity.VisualScripting;
using UnityEngine;

namespace CrazyPawn
{
    public class LineRendererConnection : MonoBehaviour
    {
        private Socket connectorA;
        private Socket connectorB;
        private LineRenderer lineRenderer;

        public void Initialize(Socket a, Socket b)
        {
            connectorA = a;
            connectorB = b;
            lineRenderer = GetComponent<LineRenderer>();

            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.07f;
            lineRenderer.endWidth = 0.07f;
            lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = Color.white };

            UpdateLineAfterConnection();
            SubscriveEvents();
        }

        private void SubscriveEvents()
        {
            connectorA.ParentPawn.OnDestroyed += DestroyConnection;
            connectorB.ParentPawn.OnDestroyed += DestroyConnection;
            
            connectorA.ParentPawn.OnPositionChanged += UpdateLineAfterConnection;
            connectorB.ParentPawn.OnPositionChanged += UpdateLineAfterConnection;
        }
        
        private void UnSubscriveEvents()
        {
            connectorA.ParentPawn.OnDestroyed -= DestroyConnection;
            connectorB.ParentPawn.OnDestroyed -= DestroyConnection;
            
            connectorA.ParentPawn.OnPositionChanged -= UpdateLineAfterConnection;
            connectorB.ParentPawn.OnPositionChanged -= UpdateLineAfterConnection;
        }

        private void UpdateLineAfterConnection()
        {
            if (connectorA == null || connectorB == null) return;
            lineRenderer.SetPosition(0, connectorA.transform.position);
            lineRenderer.SetPosition(1, connectorB.transform.position);
        }

        private void UpdateLine()
        {
            
        }


        private void DestroyConnection()
        {
            UnSubscriveEvents();
            GameManager.Instance.ConnectionManager.RemoveConnection(this); 
        }
    }
}