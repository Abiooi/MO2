using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    public class OwlIdle : FSM<OwlFSM, Owl_State>
    {
        private bool SineEnd = true;


        public OwlIdle(OwlFSM _owner)
        {
            m_Owner = _owner;
        }

        public override void Begin()
        {
            Debug.Log("Idle Begin");
            m_Owner.m_eCurState = Owl_State.Idle;
        }

        public override void Run()
        {
            if (SineEnd)
            {
                m_Owner.ChangeFSM(Owl_State.Trace);
            }
        }

        public override void Exit()
        {
            Debug.Log("Idle Exit");
            m_Owner.m_ePrevState = Owl_State.Idle;
        }
    }
}