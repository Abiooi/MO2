using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    public class HaloIdle : FSM<HaloFSM, Halo_State>
    {
        private bool sineEnd = true;

        public HaloIdle(HaloFSM _owner)
        {
            m_Owner = _owner;
        }

        public override void Begin()
        {
            Debug.Log("Idle Begin");
            m_Owner.m_eCurState = Halo_State.Idle;
        }

        public override void Run()
        {
            
            if (sineEnd)
            {
                m_Owner.ChangeFSM(Halo_State.Trace);
            }
        }

        public override void Exit()
        {
            Debug.Log("Idle Exit");
            m_Owner.m_ePrevState = Halo_State.Idle;
        }
    }
}