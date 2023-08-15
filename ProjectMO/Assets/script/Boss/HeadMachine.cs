using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    public class HeadMachine<T, StateEnum>
    {
        //������Ʈ
        private T Owner;

        //���°�
        private FSM<T, StateEnum> m_CurState = null; //����
        private FSM<T, StateEnum> m_PrevState = null; //����

        //ù ���°�
        public void Begin()
        {
            if(m_CurState != null)
            {
                m_CurState.Begin();
            }
        }
        //���� ������Ʈ
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

        //fsm����
        public void Exit()
        {
            m_CurState.Exit();
            m_CurState = null;
            m_PrevState = null;
            Debug.Log(Owner.ToString() + "FSM ����");
        }

        public void Change(FSM<T, StateEnum> _state)
        {
            //���� ���·� ��ȭ��ų �� ����
            if(_state == m_CurState)
            {
                return;
            }
            m_PrevState = m_CurState;

            //���� ���°� �ִٸ� ����
            if(m_CurState != null)
            {
                m_CurState.Exit();
            }
            m_CurState = _state;
            //���� ����� ���°� ���� �ƴϸ� ����
            if(m_CurState != null)
            {
                m_CurState.Begin();
            }
        }

        //��ȭ��ų �� ���ڰ��� ���ٸ� �� ���¸� ���
        public void Revert()
        {
            if(m_PrevState != null)
            {

                Change(m_PrevState);
            }
        }

        //���°� ����
        public void SetState(FSM<T, StateEnum> _state, T _Owner)
        {
            Owner = _Owner;
            m_CurState = _state;

            //���� ���°��� ���ݰ� �ٸ���, ������°��� ä�������� ��
            if(m_CurState != _state && m_CurState != null)
            {
                m_PrevState = m_CurState;
            }
        }
    }
}
