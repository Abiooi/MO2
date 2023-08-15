using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    public class OwlMelee : FSM<OwlFSM,Owl_State>
    {

        private Animator anim_Owl;

        public Transform target;

        private Transform objectTransform;


        public OwlMelee(OwlFSM _owner)
        {
            m_Owner = _owner;
        }
        public override void Begin()
        {
            Debug.Log("Melee Begin");
            objectTransform = m_Owner.GetComponent<Transform>();
            target = m_Owner.m_TransTarget;
            anim_Owl = m_Owner.anim_Owl;
            m_Owner.m_eCurState = Owl_State.Melee;
        }

        public override void Run()
        {
            if(!m_Owner.isAttacking && m_Owner.isFireReady)
            {
                m_Owner.StartMeleeThink();

            }

            if (Vector3.Distance(objectTransform.position, target.position) >= 10f)
            {
                //StateMachine을 원거리로 변경
                m_Owner.ChangeFSM(Owl_State.Range);
            }
        }

        public override void Exit()
        {
            Debug.Log("Melee Exit");
            m_Owner.m_ePrevState = Owl_State.Melee;
        }

        
    }
}