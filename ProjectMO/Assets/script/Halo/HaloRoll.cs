using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFSM
{
    public class HaloRoll : FSM<HaloFSM, Halo_State>
    {

        private Animator anim_Halo;

        public Transform target;

        public HaloRoll(HaloFSM _owner)
        {
            m_Owner = _owner;
        }

        public override void Begin()
        {
            Debug.Log("Roll Begin");
            anim_Halo = m_Owner.anim_Halo;
            target = m_Owner.m_TransTarget;
            m_Owner.m_eCurState = Halo_State.Roll;
            m_Owner.canGrogy = true;
            anim_Halo.SetTrigger("Roll");
            anim_Halo.SetBool("Rolling", true);
        }

        public override void Run()
        {
            if (!m_Owner.isAttacking)
            {
                m_Owner.StartRollThink();
            }
            
        }

        public override void Exit()
        {
            Debug.Log("Roll Exit");
            anim_Halo.SetBool("Rolling", false);
            m_Owner.canGrogy = false;
            m_Owner.m_ePrevState = Halo_State.Roll;
        }
    }
}