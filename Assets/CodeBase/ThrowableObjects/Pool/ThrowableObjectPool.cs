using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.ThrowableObjects.Pool
{
    public class ThrowableObjectPool : MonoBehaviour, IObjectPoolService
    {
        [SerializeField] private List<GameObject> _objectPrefabs;

        private Transform _parentObjects;
        private static List<PooledObjectInfo> _objectPools;

        private void Awake()
        {
            _parentObjects = transform;
            _objectPools = new List<PooledObjectInfo>();
        }

        public GameObject SpwanThrowableObject(Vector3 spawnPosition)
        {
            int objectToSpawnIndex = Random.Range(0, _objectPrefabs.Count);
            GameObject objectToSpawn = _objectPrefabs[objectToSpawnIndex];
            return SpwanThrowableObject(objectToSpawn, spawnPosition);
        }

        public GameObject SpwanThrowableObject(GameObject objectToSpawn, Vector3 spawnPosition)
        {
            Weapon spawnableObject = objectToSpawn.GetComponent<Weapon>();

            PooledObjectInfo pool = _objectPools.Find(p => p.LookupString == spawnableObject.GetType().Name);
            
            if (pool == null)
            {
                pool = new PooledObjectInfo() { LookupString = spawnableObject.GetType().Name };
                _objectPools.Add(pool);
            }

            GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

            if (spawnableObj == null)
            {
                spawnableObj = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
                spawnableObj.transform.SetParent(_parentObjects, true);
            }
            else
            {
                spawnableObj.transform.position = spawnPosition;
                spawnableObj.transform.SetParent(_parentObjects, true);
                pool.InactiveObjects.Remove(spawnableObj);
                spawnableObj.SetActive(true);
            }

            return spawnableObj;
        }

        public static void ReturnObjectToPool(GameObject obj)
        {
            PooledObjectInfo pool = _objectPools.Find(p => p.LookupString == obj.GetComponent<Weapon>().GetType().Name);

            if (pool == null)
            {
                Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
            }
            else
            {
                obj.SetActive(false);
                pool.InactiveObjects.Add(obj);
            }
        }
    }
}