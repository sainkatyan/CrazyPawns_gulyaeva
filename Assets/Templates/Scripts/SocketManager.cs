using System.Collections.Generic;
using UnityEngine;

namespace CrazyPawn
{
    public class SocketManager : MonoBehaviour
    {
        public List<Socket> allSockets = new List<Socket>();
        
        public bool CanConnect(Socket selectedSocket, Socket otherSocket)
        {
            return selectedSocket.transform.parent != otherSocket.transform.parent;
        }
        
        public void HighlightAvailableConnectors(Socket selectedSocket, bool highlight)
        {
            foreach (Socket socket in allSockets)
            {
                if (socket != this && CanConnect(selectedSocket, socket))
                {
                    socket.ChangeActivateMaterial(highlight);
                }
            }
        }

        public void AddSocket(Socket socket)
        {
            if (allSockets.Contains(socket)) return;
            allSockets.Add(socket);
        }

        public void RemoveSocket(Socket socket)
        {
            if (allSockets.Contains(socket)) allSockets.Remove(socket);
        }

        private void CleanSockets()
        {
            allSockets.Clear();
        }

        private void OnDestroy()
        {
            CleanSockets();
        }
    }
}