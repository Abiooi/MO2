using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFSM
{
    public class HaloTrace : FSM<HaloFSM, Halo_State>
    {

        private NavMeshAgent nav;

        private Animator anim_Halo;

        public Transform target;

        private Transform objectTransform;

        public HaloTrace(HaloFSM _owner)
        {
            m_Owner = _owner;
        }

        public override void Begin()
        {
            Debug.Log("Trace Begin");
            nav = m_Owner.GetComponent<NavMeshAgent>();
            anim_Halo = m_Owner.anim_Halo;
            objectTransform = m_Owner.GetComponent<Transform>();
            target = m_Owner.m_TransTarget;
            nav.isStopped = false;
            m_Owner.isLook = false;
            m_Owner.m_eCurState = Halo_State.Trace;
            anim_Halo.SetBool("Running", true);
        }

        public override void Run()
        {
            if (target == null)
            {
                // target이 null일 때의 처리 (예: 디버그 로그 출력 또는 다른 로직 처리)
                return;
            }

            if (Vector3.Distance(objectTransform.transform.position, target.position) <= 20f)
            {

                m_Owner.ChangeFSM(Halo_State.Melee);
            }
            else if (Vector3.Distance(objectTransform.transform.position, target.position) <= 40f)
            {

                m_Owner.ChangeFSM(Halo_State.Range);
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
            m_Owner.isLook = true;
            m_Owner.m_ePrevState = Halo_State.Trace;
            anim_Halo.SetBool("Running", false);
        }
    }
}