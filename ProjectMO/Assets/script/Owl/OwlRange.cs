using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    public class OwlRange : FSM<OwlFSM, Owl_State>
    {
        //private Enemy Head;

        public Transform target;

        private Transform objectTransform;

        

        public OwlRange(OwlFSM _owner)
        {
            m_Owner = _owner;
        }

        public override void Begin()
        {
            Debug.Log("Range Begin");
            target = m_Owner.m_TransTarget;
            objectTransform = m_Owner.transform;
            m_Owner.m_eCurState = Owl_State.Range;
        }

        public override void Run()
        {
            Debug.Log("Run");
            m_Owner.StartRangeThink();

            if (Vector3.Distance(objectTransform.position, target.position) <= 10f)
            {
                //StateMachine을 근거리로 변경
                m_Owner.ChangeFSM(Owl_State.Melee);
            }
            if (Vector3.Distance(objectTransform.position, target.position) >= 20f)
            {
                //StateMachine을 추적으로 변경
                m_Owner.ChangeFSM(Owl_State.Trace);
            }
        }

        public override void Exit()
        {
            Debug.Log("Range Exit");
            m_Owner.m_ePrevState = Owl_State.Range;
        }

        
    }
}
