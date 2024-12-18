using CodeBase.ThrowableObjects.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.ThrowableObjects.Pool
{
    public class ThrowableObjectPool : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _objectPrefabs;

        private List<PooledObjectInfo> _objectPools;

        private const int PoolBulkAmountPerObject = 5;

        private void Awake()
        {
            _objectPools = new List<PooledObjectInfo>();
            PoolAllObjects();
        }

        public GameObject SpawnThrowableObject(Vector3 spawnPosition)
        {
            int objectPrefabIndex = Random.Range(0, _objectPrefabs.Count);
            GameObject objectPrefab = _objectPrefabs[objectPrefabIndex];
            return SpawnThrowableObject(objectPrefab.GetComponent<ThrowableObjectBase>(), spawnPosition);
        }

        private GameObject SpawnThrowableObject(ThrowableObjectBase objectPrefab, Vector3 spawnPosition)
        {
            PooledObjectInfo pool = _objectPools.Find(p => p.LookupString == objectPrefab.GetType().Name);
            
            if (pool == null)
            {
                Debug.LogError($"Can't find pool with LookupString = {objectPrefab.GetType().Name}");
                return null;
            }

            ThrowableObjectBase obj = pool.InactiveObjects.FirstOrDefault();

            if (obj == null)
            {
                obj = CreateThrowableObject(objectPrefab);
            }
            else
            {
                pool.InactiveObjects.Remove(obj);
                obj.gameObject.SetActive(true);
            }

            obj.transform.position = spawnPosition;
            obj.Disabled += ReturnObjectToPool;

            return obj.gameObject;
        }

        private void ReturnObjectToPool(ThrowableObjectBase obj)
        {
            PooledObjectInfo pool = _objectPools.Find(p => p.LookupString == obj.GetType().Name);

            if (pool == null)
            {
                Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
            }
            else
            {
                obj.gameObject.SetActive(false);
                pool.InactiveObjects.Add(obj);
                obj.Disabled -= ReturnObjectToPool;
            }
        }

        private void PoolAllObjects()
        {
            foreach (GameObject objPrefab in _objectPrefabs)
            {
                ThrowableObjectBase throwableObject = objPrefab.GetComponent<ThrowableObjectBase>();

                PooledObjectInfo pool = new(throwableObject.GetType().Name);
                _objectPools.Add(pool);

                for (int i = 0; i < PoolBulkAmountPerObject; i++)
                {
                    ThrowableObjectBase obj = CreateThrowableObject(throwableObject);
                    obj.gameObject.SetActive(false);
                    pool.InactiveObjects.Add(obj);
                }
            }
        }

        private ThrowableObjectBase CreateThrowableObject(ThrowableObjectBase objPrefab)
        {
            ThrowableObjectBase obj = Instantiate(objPrefab);
            obj.Initialize(transform);
            return obj;
        }
    }
}