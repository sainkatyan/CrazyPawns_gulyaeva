using System.Collections.Generic;
using UnityEngine;
namespace CrazyPawn
{
    public class SocketManager : MonoBehaviour
    {
        private List<Socket> activatedSockets = new List<Socket>();

        public void Add(Socket socket)
        {
            activatedSockets.Add(socket);
        }

        public void Remove(Socket socket)
        {
            activatedSockets.Remove(socket);
        }

        public void ClearActivatedSockets()
        {
            activatedSockets.Clear();
        }

        public void CreateActivationLine()
        {
            
        }
    }
}