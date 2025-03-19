using System.Collections.Generic;
using UnityEngine;

namespace CrazyPawn
{
    public class ConnectionManager : MonoBehaviour
    {
        [SerializeField] private GameObject connectionPrefab;
        [SerializeField] private Transform poolConnections;
        private readonly List<LineRendererConnection> connections = new();
        private SpawnFactory<LineRendererConnection> connectionSpawnFactory;

        private void Awake()
        {
            connectionSpawnFactory = new SpawnFactory<LineRendererConnection>(connectionPrefab, poolConnections);
        }

        public void CreateConnection(Socket a, Socket b)
        {
            if (a == b) return;

            LineRendererConnection connection = connectionSpawnFactory.Get();
            connection.Initialize(a, b);
            connections.Add(connection);
        }

        public void RemoveConnection(LineRendererConnection connection)
        {
            connections.Remove(connection);
            connectionSpawnFactory.Release(connection);
        }
    }
}