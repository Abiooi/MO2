using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFSM
{
    public class OwlTrace : FSM<OwlFSM, Owl_State>
    {
        private OwlFSM m_Owner;

        private NavMeshAgent nav;

        private Animator anim_Owl;

        public Transform target;

        private Transform objectTransform;

        public OwlTrace(OwlFSM _owner)
        {
            m_Owner = _owner;
        }

        public override void Begin()
        {
            Debug.Log("Trace Begin");
            nav = m_Owner.GetComponent<NavMeshAgent>();
            anim_Owl = m_Owner.anim_Owl;
            objectTransform = m_Owner.GetComponent<Transform>();
            target = m_Owner.m_TransTarget;
            nav.isStopped = false;
            m_Owner.m_eCurState = Owl_State.Trace;
            anim_Owl.SetBool("Running", true);
        }

        public override void Run()
        {
            if (target == null)
            {
                // target이 null일 때의 처리 (예: 디버그 로그 출력 또는 다른 로직 처리)
                return;
            }

            if (Vector3.Distance(objectTransform.transform.position, target.position) <= 10f)
            {
                
                m_Owner.ChangeFSM(Owl_State.Melee);
            }
            else if (Vector3.Distance(objectTransform.transform.position, target.position) <= 20f)
            {
               
                m_Owner.ChangeFSM(Owl_State.Range);
            }
            else
            {
                nav.SetDestination(target.position);

            }
        }

        public override void Exit()
        {
            Debug.Log("Trace Exit");
            nav.isStopped = true;
            m_Owner.m_ePrevState = Owl_State.Trace;
            anim_Owl.SetBool("Running", false);
        }
    }
}