using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    public class HaloMelee : FSM<HaloFSM, Halo_State>
    {

        private Animator anim_Halo;

        public Transform target;

        private Transform objectTransform;


        public HaloMelee(HaloFSM _owner)
        {
            m_Owner = _owner;
        }
        public override void Begin()
        {
            Debug.Log("Melee Begin");
            objectTransform = m_Owner.GetComponent<Transform>();
            target = m_Owner.m_TransTarget;
            anim_Halo = m_Owner.anim_Halo;
            m_Owner.m_eCurState = Halo_State.Melee;
        }

        public override void Run()
        {
            if (!m_Owner.isAttacking)
            {
                m_Owner.StartMeleeThink();

            }

            if (Vector3.Distance(objectTransform.position, target.position) >= 20f)
            {
                //StateMachine을 원거리로 변경
                m_Owner.ChangeFSM(Halo_State.Range);

            }
        }

        public override void Exit()
        {
            Debug.Log("Melee Exit");
            m_Owner.m_ePrevState = Halo_State.Melee;
        }


    }
}