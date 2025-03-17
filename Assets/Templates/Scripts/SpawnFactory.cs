using UnityEngine;

namespace CrazyPawn
{
    public class SpawnFactory
    {
        private GameObject prefab;

        public SpawnFactory(GameObject prefab)
        {
            this.prefab = prefab;
        }

        public GameObject Create(Vector3 position, Quaternion rotation)
        {
            GameObject obj = Object.Instantiate(prefab, position, rotation);
            ISpawnable spawnable = obj.GetComponent<ISpawnable>();
            spawnable?.OnSpawn();
            
            return obj;
        }
    }
}