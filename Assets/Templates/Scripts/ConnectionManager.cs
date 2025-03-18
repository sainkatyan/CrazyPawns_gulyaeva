using System.Collections.Generic;
using UnityEngine;

namespace CrazyPawn
{
    public class ConnectionManager : MonoBehaviour
    {
        [SerializeField] private GameObject connectionPrefab;
        private readonly List<LineRendererConnection> connections = new();
        
        public void CreateConnection(Socket a, Socket b)
        {
            GameObject connectionObject = Instantiate(connectionPrefab);
            LineRendererConnection connection = connectionObject.GetComponent<LineRendererConnection>();
            connection.Initialize(a, b);
            connections.Add(connection);
        }
    }
}