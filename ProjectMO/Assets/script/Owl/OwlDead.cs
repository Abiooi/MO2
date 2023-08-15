using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    public class OwlDead : FSM<OwlFSM, Owl_State>
    {

        public OwlDead(OwlFSM _owner)
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