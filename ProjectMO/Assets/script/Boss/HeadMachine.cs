using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    public class HeadMachine<T, StateEnum>
    {
        //오브젝트
        private T Owner;

        //상태값
        private FSM<T, StateEnum> m_CurState = null; //현재
        private FSM<T, StateEnum> m_PrevState = null; //이전

        //첫 상태값
        public void Begin()
        {
            if(m_CurState != null)
            {
                m_CurState.Begin();
            }
        }
        //상태 업데이트
        public void Run()
        {
            CheckState();
        }

        public void CheckState()
        {
            if(m_CurState != null)
            {
                m_CurState.Run();
            }
        }

        //fsm종료
        public void Exit()
        {
            m_CurState.Exit();
            m_CurState = null;
            m_PrevState = null;
            Debug.Log(Owner.ToString() + "FSM 종료");
        }

        public void Change(FSM<T, StateEnum> _state)
        {
            //같은 상태로 변화시킬 시 리턴
            if(_state == m_CurState)
            {
                return;
            }
            m_PrevState = m_CurState;

            //현재 상태가 있다면 종료
            if(m_CurState != null)
            {
                m_CurState.Exit();
            }
            m_CurState = _state;
            //새로 적용된 상태가 널이 아니면 실행
            if(m_CurState != null)
            {
                m_CurState.Begin();
            }
        }

        //변화시킬 때 인자값이 없다면 전 상태를 출력
        public void Revert()
        {
            if(m_PrevState != null)
            {

                Change(m_PrevState);
            }
        }

        //상태값 세팅
        public void SetState(FSM<T, StateEnum> _state, T _Owner)
        {
            Owner = _Owner;
            m_CurState = _state;

            //들어온 상태값이 지금과 다를때, 현재상태값이 채워져있을 때
            if(m_CurState != _state && m_CurState != null)
            {
                m_PrevState = m_CurState;
            }
        }
    }
}
