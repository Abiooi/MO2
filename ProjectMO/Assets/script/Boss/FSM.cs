using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM
{
    abstract public class FSM<T, StateEnum>
    {
        protected T m_Owner;
        abstract public void Begin();
        abstract public void Run();
        abstract public void Exit();
    }

    public enum Owl_State
    {
        Idle,
        Trace,
        Melee,
        Range,
        Dead,
        END
    }

    public enum Halo_State
    {
        Idle,
        Trace,
        Roll,
        Melee,
        Range,
        Grogy,
        Dead,
        END
    }

    public enum Magelan_State
    {
        Idle,
        Melee,
        Range,
        Dead,
        END
    }

}
