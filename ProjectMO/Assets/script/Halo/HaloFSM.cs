using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFSM
{
    public class HaloFSM : MonoBehaviour
    {
        public HeadMachine<HaloFSM, Halo_State> m_state;
        public FSM<HaloFSM, Halo_State>[] m_arrState = new FSM<HaloFSM, Halo_State>[(int)Halo_State.END];

        public Transform m_TransTarget;
        public EffectController effectController;

        public float maxHealth;
        public float curHealth;

        public GameObject EnemyCanvasGo;

        public Halo_State m_eCurState;
        public Halo_State m_ePrevState;

        public NavMeshAgent nav;
        public Animator anim_Halo;

        public BoxCollider meleeArea;
        public BoxCollider tailArea;
        public BoxCollider idleArea;
        public BoxCollider rollArea;
        public BoxCollider fallArea;

        public Rigidbody rigid;

        public ParticleSystem ps;

        public bool isFireReady = true;

        public bool isLook;

        public bool isAttacking;

        public bool isDead;

        public bool canGrogy = false;

        private bool jumpkoongRun = false;

        private bool irontailRun = false;

        bool isHitting;

        public float rate;

        public float fireDelay;

        public float rotationSpeed = 2.0f;

        public float grogyP = 0.5f;

        Vector3 dashVec;

        //public OwlFSM()
        //{
        //    Init();
        //}

        public void Init()
        {
            m_state = new HeadMachine<HaloFSM, Halo_State>();

            m_arrState[(int)Halo_State.Idle] = new HaloIdle(this);
            m_arrState[(int)Halo_State.Trace] = new HaloTrace(this);
            m_arrState[(int)Halo_State.Roll] = new HaloRoll(this);
            m_arrState[(int)Halo_State.Melee] = new HaloMelee(this);
            m_arrState[(int)Halo_State.Range] = new HaloRange(this);
            m_arrState[(int)Halo_State.Grogy] = new HaloGrogy(this);
            m_arrState[(int)Halo_State.Dead] = new HaloDead(this);

            m_state.SetState(m_arrState[(int)Halo_State.Idle], this);
        }

        private void Awake()
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            anim_Halo = this.GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();
            rigid = GetComponent<Rigidbody>();
            effectController = GetComponent<EffectController>();
            Init();
            ChangeFSM(Halo_State.Trace);

        }

        private void Start()
        {
            effectController.StopEffects();
            Begin();
            //ps.Stop();
            fireDelay = 0f;
        }

        private void Update()
        {
            if(fireDelay <= 3)
            {
                fireDelay += Time.deltaTime;
            }
            isFireReady = rate < fireDelay;
            Run();


            if (isLook && !isDead)
            {
                Vector3 directionToTarget = m_TransTarget.position - transform.position;

                // 현재 게임 오브젝트의 회전 각도를 계산
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                // 부드러운 회전을 위해 현재 회전에서 목표 회전으로 보간
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        public void ChangeFSM(Halo_State ps)
        {
            for (int i = 0; i < (int)Halo_State.END; ++i)
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
        public void StartRollThink()
        {
            StartCoroutine("RollThink");
        }

        IEnumerator RollThink()
        {
            //boxCollider.enabled = false;
            if (isFireReady && !isAttacking)
            {
                fireDelay = 0f;
                yield return new WaitForSeconds(1f);
                int ranAction = Random.Range(0, 2);
                switch (ranAction)
                {
                    case 0:
                        
                        StartCoroutine("Irontail");
                        break;

                    case 1:
                        
                        StartCoroutine("Jumpkoong");
                        break;

                }
                isFireReady = false;
                yield return new WaitForSeconds(0.1f);
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

                        StopCoroutine("Swing");
                        StartCoroutine("Swing");
                        
                        break;

                    case 1:

                        StopCoroutine("Downswing");
                        StartCoroutine("Downswing");
                        
                        break;

                    case 2:
                        StopCoroutine("Claw");
                        StartCoroutine("Claw");
                        
                        break;
                }
                isFireReady = false;
                yield return new WaitForSeconds(0.1f);
            }
        }
        IEnumerator Irontail()
        {
            if (jumpkoongRun || irontailRun)
            {
                yield break;
            }
            irontailRun = true;
            Debug.Log("irontail start");
            rate = 3f;
            dashVec = transform.forward;
            rigid.AddForce(dashVec * 5, ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);
            rigid.AddForce(-rigid.velocity, ForceMode.VelocityChange);
            anim_Halo.SetTrigger("Irontail");
            anim_Halo.SetBool("Rolling", false);
            anim_Halo.SetTrigger("Rollback");
            isAttacking = true;
            tailArea.enabled = true;
            yield return new WaitForSeconds(1f);
            tailArea.enabled = false;
            isAttacking = false;
            irontailRun = false;
            if (!canGrogy)
            {
                yield break;
            }
            else
            {
                ChangeFSM(Halo_State.Trace);
            }
            yield return new WaitForSeconds(0.1f);
        }
        IEnumerator Jumpkoong()
        {
            
            if (jumpkoongRun || irontailRun)
            {
                yield break;
            }
            Debug.Log("jumpkoong Start");
            jumpkoongRun = true;
            isAttacking = true;
            nav.enabled = false;
            rate = 3f;
            yield return new WaitForSeconds(0.1f);
            Vector3 jumpDir = transform.up;
            rigid.AddForce(jumpDir * 50f, ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
            yield return new WaitForSeconds(0.1f);
            Vector3 diveDir = (m_TransTarget.position - transform.position).normalized;
            rigid.AddForce(diveDir * 100f, ForceMode.Impulse);
            fallArea.enabled = true;
            yield return new WaitForSeconds(0.5f);
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
            fallArea.enabled = false;
            anim_Halo.SetTrigger("Stuck");
            anim_Halo.SetBool("Rolling", false);
            anim_Halo.SetTrigger("Rollback");
            yield return new WaitForSeconds(1f);
            nav.enabled = true;
            nav.isStopped = true;
            isAttacking = false;
            jumpkoongRun = false;
            if (!canGrogy)
            {
                yield break;
            }
            else
            {
                ChangeFSM(Halo_State.Trace);
            }
            yield return new WaitForSeconds(0.1f);
        }
        IEnumerator Swing()
        {
            rate = 3f;
            anim_Halo.SetTrigger("Swing");
            isLook = false;
            isAttacking = true;
            meleeArea.enabled = true;
            effectController.PlayEffects();
            yield return new WaitForSeconds(0.5f);
            meleeArea.enabled = false;
            isAttacking = false;
            effectController.StopEffects();
            isLook = true;
            isFireReady = false;
            //isLook = false;
            //isAttacking = true;
            //yield return new WaitForSeconds(0.5f);
            //meleeArea.enabled = true;

            ////noTrace = true;
            //rate = 3f;
            ////1
            ////trailEffect.enabled = true;

            ////2
            //yield return new WaitForSeconds(0.3f);
            //meleeArea.enabled = false;
            //isAttacking = false;
            //isLook = true;
        }

        IEnumerator Downswing()
        {
            rate = 3f;
            anim_Halo.SetTrigger("Downswing");
            yield return new WaitForSeconds(0.3f);
            isLook = false;
            isAttacking = true;
            tailArea.enabled = true;
            yield return new WaitForSeconds(0.5f);
            tailArea.enabled = false;
            isAttacking = false;
            isLook = true;
            ////noTrace = true;
            //rate = 3f;
            ////1
            //yield return new WaitForSeconds(0.15f);
            //isLook = false;
            //isAttacking = true;
            ////trailEffect.enabled = true;

            //yield return new WaitForSeconds(0.95f);
            //meleeArea.enabled = true;

            //yield return new WaitForSeconds(0.2f);
            //meleeArea.enabled = false;
            //isAttacking = false;
            //isLook = true;
        }

        IEnumerator Claw()
        {
            rate = 3f;
            anim_Halo.SetTrigger("Claw");
            yield return new WaitForSeconds(0.1f);
            isLook = false;
            meleeArea.enabled = true;
            yield return new WaitForSeconds(0.5f);
            isLook = true;
            meleeArea.enabled = false;
        }

        public void StartGrogyEndTime()
        {
            StartCoroutine("GrogyEndTime");
        }
        IEnumerator GrogyEndTime()
        {
            yield return new WaitForSeconds(3f);
            canGrogy = true;

        }
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.collider.gameObject.CompareTag("Tree") && m_eCurState == Halo_State.Roll)
            {
                Debug.Log("Grogii");
                ChangeFSM(Halo_State.Grogy);
            }
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Melee" && !isDead && !isHitting)
            {
                Weapon weapon = other.GetComponent<Weapon>();
                isHitting = true;

                curHealth -= weapon.damage * grogyP;
                EnemyCanvasGo.GetComponent<EnemyHpBar>().Boss_Dmg(weapon.damage * grogyP);

                StartCoroutine("OnDamage");
                Debug.Log("Melee : " + curHealth);
                isHitting = false;
            }
            //if(other.tag == "Tree" && canGrogy == true)
            //{
            //    Debug.Log("Grogii");
            //    ChangeFSM(Halo_State.Grogy);
            //}
        }
        
        IEnumerator OnDamage()
        {

            if (curHealth > 0)
            {
                //mat.color = Color.white;
                if (!isAttacking)
                {
                    anim_Halo.SetTrigger("Hitting");
                    isHitting = true;
                    yield return new WaitForSeconds(0.2f);
                    isHitting = false;

                }
                isDead = false;
            }
            //mat.color = Color.red;
            else
            {
                //mat.color = Color.gray;
                anim_Halo.SetTrigger("Yoodahee");
                //target = null;
                ChangeFSM(Halo_State.Dead);
                isDead = true;
                Destroy(gameObject, 4);

            }
        }
    }
}
