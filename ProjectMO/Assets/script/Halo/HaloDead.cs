using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    public class HaloDead : FSM<HaloFSM, Halo_State>
    {

        public HaloDead(HaloFSM _owner)
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