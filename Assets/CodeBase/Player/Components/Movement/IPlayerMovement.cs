using UnityEngine;

namespace CodeBase.Player.Components.Movement
{
    public interface IPlayerMovement
    {
        void Move(Vector2 inputVector);
    }
}