using UnityEngine;

namespace CrazyPawn
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public CrazyPawnSettings Settings;
        public Chessboard Chessboard;
        public PawnController PawnController;
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