using UnityEngine;

namespace CodeBase.Player.Components.Aim
{
    public interface IPlayerAim
    {
        Vector2 CurrentCoords { get; }

        void UpdateAimCoords(Vector2 newCoords);
    }
}