using UnityEngine;

namespace CrazyPawn
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private CrazyPawnSettings settings;
        [SerializeField] private GameObject spawnPrefab;

        private void Start()
        {
            SpawnPawnsObject();
        }

        private void SpawnPawnsObject()
        {
            if (spawnPrefab == null) return;
            for (int i = 0; i < settings.InitialPawnCount; i++)
            {
                Vector3 spawnPosition = GetRandomPointInCircle();
                Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
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