using System;
using CodeBase.Character;

namespace CodeBase.Knight
{
    public class KnightMain : CharacterMain
    {
        public event Action Died;

        internal override void Dead()
        {
            Died?.Invoke();
        }
    }
}