using UnityEngine;
using UnityEngine.Serialization;

namespace CrazyPawn
{
    public class PawnController : MonoBehaviour
    {
        [SerializeField] private Pawn pawn;
        
        private CrazyPawnSettings settings;
        private SpawnFactory factory;
        
        public void Init(CrazyPawnSettings settings, Chessboard chessboard)
        {
            this.settings = settings;

            factory = new SpawnFactory(pawn.gameObject);
            pawn.SetChessBoardLimit(chessboard.GetChessboardLimit());
            SpawnPawnObjects();
        }

        private void SpawnPawnObjects()
        {
            if (pawn == null) return;
            for (int i = 0; i < settings.InitialPawnCount; i++)
            {
                Vector3 spawnPosition = GetRandomPointInCircle();
                factory.Create(spawnPosition, Quaternion.identity);
            }
        }
        
        private Vector3 GetRandomPointInCircle()
        {
            float angle = Random.Range(0f, Mathf.PI * 2); 
            float radius = Mathf.Sqrt(Random.Range(0f, 1f)) * settings.InitialZoneRadius; 

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            return new Vector3(x, 0, z);
        }
    }
}