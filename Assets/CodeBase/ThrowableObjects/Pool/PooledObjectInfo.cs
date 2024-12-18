using CodeBase.ThrowableObjects.Core;
using System.Collections.Generic;

namespace CodeBase.ThrowableObjects.Pool
{
    public class PooledObjectInfo
    {
        public string LookupString;
        public List<ThrowableObjectBase> InactiveObjects = new();

        public PooledObjectInfo(string lookupString)
        {
            LookupString = lookupString;
        }
    }
}