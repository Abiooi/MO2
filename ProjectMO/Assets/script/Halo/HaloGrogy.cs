using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    public class HaloGrogy : FSM<HaloFSM, Halo_State>
    {
        private Animator anim_Halo;
        public HaloGrogy(HaloFSM _owner)
        {
            m_Owner = _owner;
        }

        public override void Begin()
        {
            Debug.Log("Grogy Start");
            anim_Halo = m_Owner.anim_Halo;
            m_Owner.m_eCurState = Halo_State.Grogy;
            m_Owner.anim_Halo.SetTrigger("Grogy");
            m_Owner.grogyP = 3;
            m_Owner.canGrogy = false;
            m_Owner.isLook = false;
        }

        public override void Run()
        {
            anim_Halo.speed *= 0.5f;
            m_Owner.StartGrogyEndTime();
            anim_Halo.speed *= 2f;
            if (m_Owner.canGrogy)
            {
                m_Owner.ChangeFSM(Halo_State.Trace);
            }
            
        }

        public override void Exit()
        {
            m_Owner.grogyP = 0.5f;
            m_Owner.isLook = true;
            m_Owner.m_ePrevState = Halo_State.Grogy;
            Debug.Log("Grogy Exit");
        }
    }
}

