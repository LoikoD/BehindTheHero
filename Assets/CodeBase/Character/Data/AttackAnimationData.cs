using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Character.Data
{
    [System.Serializable]
    public class KnightAttackAnimationData
    {
        public float PoleaxeHitDelay = 0.1334f;
        public float SwordHitDelay = 0.1334f;
        public float FistsFirstHitDelay = 0.0667f;
        public float FistsHitInterval = 0.2001f;
    }

    [System.Serializable]
    public class EnemyAttackAnimationData
    {
        public float HitDelay = 0.3332f;
    }
}
