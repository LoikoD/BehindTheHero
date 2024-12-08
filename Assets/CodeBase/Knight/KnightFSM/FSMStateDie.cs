using CodeBase.Infrastructure.States;
using CodeBase.Logic.Utilities;
using CodeBase.Logic;
using CodeBase.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace CodeBase.Knight.KnightFSM
{
    public class FSMStateDie : IFsmState
    {
        private readonly KnightStateMachine _knightStateMachine;

        public FSMStateDie(KnightStateMachine knightStateMachine)
        {
            _knightStateMachine = knightStateMachine;
        }

        public void Enter()
        {
            _knightStateMachine.SetDieFlag();
        }

        public void Update()
        {

        }

        public void Exit()
        {

        }
    }
}
