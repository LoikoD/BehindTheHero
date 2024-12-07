using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.ThrowableObjects.Pool
{
    public class PooledObjectInfo
    {
        public string LookupString;
        public List<GameObject> InactiveObjects = new();
    }
}