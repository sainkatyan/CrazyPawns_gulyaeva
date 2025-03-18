using UnityEngine;

namespace CrazyPawn
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CrazyPawnSettings settings;
        [SerializeField] private Chessboard chessboard;
        [SerializeField] private PawnController pawnController;
        private void Start()
        {
            chessboard.Init(settings);
            pawnController.Init(settings, chessboard);
        }
    }
}