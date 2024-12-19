using UnityEngine;

namespace CodeBase.Player.Components.Thrower
{
    public interface IPlayerItemThrower
    {
        void Throw(Vector2 targetPoint);
    }
}