using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CrazyPawn
{
    public class SpawnFactory<T> where T : MonoBehaviour
    {
        private GameObject prefab;
        private Transform parent;
        private Queue<T> pool = new();

        public SpawnFactory(GameObject prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
        }
        
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

        public T Get()
        {
            if (pool.Count > 0)
            {
                T obj = pool.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            GameObject instance = GameObject.Instantiate(prefab, parent);
            return instance.GetComponent<T>();
        }
        
        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}