using CrazyPawn;
using Templates.Scripts.Pawn;
using Templates.Scripts.Socket;
using UnityEngine;

namespace Templates.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public CrazyPawnSettings Settings;
        public Chessboard Chessboard;
        public PawnManager PawnManager;
        public ConnectionManager ConnectionManager;
        public SocketManager SocketManager;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
    }
}