using System.Collections.Generic;
using Templates.Scripts.Factory;
using Templates.Scripts.Socket;
using UnityEngine;

namespace CrazyPawn
{
    public class ConnectionManager : MonoBehaviour
    {
        [SerializeField] private GameObject connectionPrefab;
        [SerializeField] private Transform poolConnections;
        private readonly List<ConnectionView> connections = new();
        private SpawnFactory<ConnectionView> connectionSpawnFactory;

        private void Awake()
        {
            connectionSpawnFactory = new SpawnFactory<ConnectionView>(connectionPrefab, poolConnections);
        }

        public void CreateConnection(Socket a, Socket b)
        {
            if (a == b) return;

            ConnectionView connectionView = connectionSpawnFactory.Get();
            connectionView.Initialize(a, b);
            connections.Add(connectionView);
        }

        public void RemoveConnection(ConnectionView connectionView)
        {
            connections.Remove(connectionView);
            connectionSpawnFactory.Release(connectionView);
        }
    }
}