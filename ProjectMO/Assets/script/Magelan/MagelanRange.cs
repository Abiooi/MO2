using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    public class MagelanRange : FSM<MagelanFSM, Magelan_State>
    {

        public MagelanRange(MagelanFSM _owner)
        {
            m_Owner = _owner;
        }

        public override void Begin()
        {

        }

        public override void Run()
        {


        }

        public override void Exit()
        {

        }
    }

}