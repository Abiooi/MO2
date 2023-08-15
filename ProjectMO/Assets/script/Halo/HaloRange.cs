using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    public class HaloRange : FSM<HaloFSM, Halo_State>
    {
        //private Enemy Head;

        public Transform target;

        private Transform objectTransform;

        private Animator anim_Halo;

        public HaloRange(HaloFSM _owner)
        {
            m_Owner = _owner;
        }

        public override void Begin()
        {
            Debug.Log("Range Begin");
            target = m_Owner.m_TransTarget;
            objectTransform = m_Owner.transform;
            m_Owner.m_eCurState = Halo_State.Range;
            anim_Halo = m_Owner.anim_Halo;
        }

        public override void Run()
        {
            Debug.Log("Run");
            
           

            if (Vector3.Distance(objectTransform.position, target.position) <= 20f)
            {
                //StateMachine을 근거리로 변경
                m_Owner.ChangeFSM(Halo_State.Melee);
            }
            else if (Vector3.Distance(objectTransform.position, target.position) >= 40f)
            {
                //StateMachine을 추적으로 변경
                m_Owner.ChangeFSM(Halo_State.Trace);
            }
            else
            {
                m_Owner.ChangeFSM(Halo_State.Roll);
            }
        }

        public override void Exit()
        {
            Debug.Log("Range Exit");
            m_Owner.m_ePrevState = Halo_State.Range;
        }


    }
}
