using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.ThrowableObjects.Pool
{
    public interface IObjectPoolService : IService
    {
        GameObject SpwanThrowableObject(Vector3 spawnPosition);
    }
}