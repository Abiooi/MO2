using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFSM
{
    public class OwlFSM : MonoBehaviour
    {
        public HeadMachine<OwlFSM, Owl_State> m_state;
        public FSM<OwlFSM, Owl_State>[] m_arrState = new FSM<OwlFSM, Owl_State>[(int)Owl_State.END];

        public Transform m_TransTarget;

        public Owl_State m_eCurState;
        public Owl_State m_ePrevState;

        public NavMeshAgent nav;
        public Animator anim_Owl;

        public Enemy Head;

        public BoxCollider meleeArea;

        public Rigidbody rigid;

        public ParticleSystem ps;

        public bool isFireReady = true;

        public bool isLook;

        public bool isAttacking;

        public float rate;

        public float fireDelay;

        Vector3 dashVec;

        //public OwlFSM()
        //{
        //    Init();
        //}

        public void Init()
        {
            m_state = new HeadMachine<OwlFSM, Owl_State>();

            m_arrState[(int)Owl_State.Idle] = new OwlIdle(this);
            m_arrState[(int)Owl_State.Trace] = new OwlTrace(this);
            m_arrState[(int)Owl_State.Melee] = new OwlMelee(this);
            m_arrState[(int)Owl_State.Range] = new OwlRange(this);
            m_arrState[(int)Owl_State.Dead] = new OwlDead(this);

            m_state.SetState(m_arrState[(int)Owl_State.Idle], this);
        }

        private void Awake()
        {
            anim_Owl = this.GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();
            Head = GetComponent<Enemy>();
            rigid = GetComponent<Rigidbody>();
            Init();
            ChangeFSM(Owl_State.Trace);
            
        }

        private void Start()
        {
            Begin();
            ps.Stop();
            fireDelay = 0f;
        }

        private void Update()
        {
            fireDelay += Time.deltaTime;
            isFireReady = rate < fireDelay;
            Run();
        }

        public void ChangeFSM(Owl_State ps)
        {
            for (int i = 0; i < (int)Owl_State.END; ++i)
            {
                if (i == (int)ps)
                {
                    m_state.Change(m_arrState[(int)ps]);
                }
            }
        }

        public void Begin()
        {
            m_state.Begin();
        }

        public void Run()
        {
            m_state.Run();
        }

        public void Exit()
        {
            m_state.Exit();
        }
        public void StartMeleeThink()
        {
            StartCoroutine("MeleeThink");
        }
        public void StartRangeThink()
        {
            StartCoroutine("RangeThink");
        }

        IEnumerator RangeThink()
        {
            //boxCollider.enabled = false;
            if (isFireReady)
            {
                fireDelay = 0f;
                rate = 6f;
                isLook = false;
                isAttacking = true;
                //noTrace = true;
                anim_Owl.SetTrigger("Dashing");
                ps.Play();
                yield return new WaitForSeconds(0.5f);
                ps.Stop();
                dashVec = transform.forward;
                yield return new WaitForSeconds(1f);
                rigid.AddForce(dashVec * 20, ForceMode.Impulse);
                meleeArea.enabled = true;
                int dotCount = 10;
                for (int i = 0; i < dotCount; i++)
                {
                    meleeArea.enabled = !meleeArea.enabled;

                    yield return new WaitForSeconds(0.2f);
                }

                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(-rigid.velocity, ForceMode.VelocityChange);
                meleeArea.enabled = false;
                yield return new WaitForSeconds(1f);
                isAttacking = false;
                //noTrace = false;
                isLook = true;

            }
        }

        IEnumerator MeleeThink()
        {
            if (isFireReady)
            {

                fireDelay = 0f;
                yield return new WaitForSeconds(0.1f);

                int ranAction = Random.Range(0, 3);

                switch (ranAction)
                {
                    case 0:

                        StopCoroutine("Wing");
                        StartCoroutine("Wing");
                        isFireReady = false;
                        break;

                    case 1:
                        StopCoroutine("Wing");
                        StartCoroutine("Wing");
                        isFireReady = false;
                        break;

                    case 2:
                        anim_Owl.SetTrigger("Kicking");
                        isAttacking = true;
                        StopCoroutine("Kick");
                        StartCoroutine("Kick");
                        isFireReady = false;
                        break;
                }
            }
        }
        IEnumerator Wing()
        {
            anim_Owl.SetTrigger("WingSlash");
            isLook = false;
            isAttacking = true;
            yield return new WaitForSeconds(0.5f);
            meleeArea.enabled = true;

            //noTrace = true;
            rate = 3f;
            //1
            //trailEffect.enabled = true;

            //2
            yield return new WaitForSeconds(0.3f);
            meleeArea.enabled = false;
            isAttacking = false;
            isLook = true;
        }

        IEnumerator Kick()
        {
            //noTrace = true;
            rate = 3f;
            //1
            yield return new WaitForSeconds(0.15f);
            isLook = false;
            isAttacking = true;
            //trailEffect.enabled = true;

            yield return new WaitForSeconds(0.95f);
            meleeArea.enabled = true;

            yield return new WaitForSeconds(0.2f);
            meleeArea.enabled = false;
            isAttacking = false;
            isLook = true;
        }
    }
}
