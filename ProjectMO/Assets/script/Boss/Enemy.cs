using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
    //public enum State 
    //{
    //    Idle,
    //    Trace,
    //    Melee,
    //    Range,
    //    Dead
    //};
    //public State state = State.Trace;

    public float maxHealth;
    public float curHealth;

    public GameObject EnemyCanvasGo;
    public Transform target;

    bool isAttacking;
    bool isFireReady = true;
    bool isHitting;
    bool noTrace = false;
    bool isDead;
    bool isLook;
    public float rate;

    Rigidbody rigid;
    BoxCollider boxCollider;
    //Material mat;
    NavMeshAgent nav;
    Animator anim_boss;

    Vector3 lookVec;
    Vector3 dashVec;
    Vector3 breakVec;

    float fireDelay;
    
    //public BoxCollider meleeArea;
    //public TrailRenderer trailEffect;


    void Awake()
    {
        anim_boss = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        //mat = GetComponent<SkinnedMeshRenderer>().material;
        //nav = GetComponent<NavMeshAgent>();

        //nav.destination = target.position;
        //StartCoroutine(StateMachine());
    }
    void Start()
    {
        //ps.Stop();
        //fireDelay = 0f;
    }

    //IEnumerator StateMachine()
    //{
    //    while(curHealth > 0)
    //    {
    //        yield return StartCoroutine(state.ToString());
    //    }
    //}

    //IEnumerator Idle()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    noTrace = false;
    //    ChangeState(State.Trace);
    //}

    //IEnumerator Trace()
    //{
    //    anim_boss.SetBool("Running", true);
    //    if (Vector3.Distance(transform.position, target.position) <= 10f)
    //    {
    //        anim_boss.SetBool("Running", false);
    //        noTrace = true;
    //        ChangeState(State.Melee);
    //    }
    //    else if(Vector3.Distance(transform.position, target.position) <= 20f)
    //    {
    //        anim_boss.SetBool("Running", false);
    //        noTrace = true;
    //        ChangeState(State.Range);
    //    }
    //    else
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}

    //IEnumerator Melee()
    //{
    //    if (!isAttacking && isFireReady)
    //    {
    //        isLook = true;
    //        noTrace = true;
    //        StartCoroutine("MeleeThink");
    //    }


    //    if (Vector3.Distance(transform.position, target.position) >= 10f)
    //    {
    //        //StateMachine을 원거리로 변경
    //        ChangeState(State.Range);
    //    }
    //    else
    //    {
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    //IEnumerator Range()
    //{
    //    if (!isAttacking && isFireReady)
    //    {
    //        isLook = true;
    //        noTrace = true;
    //        StartCoroutine("RangeThink");
    //    }


    //    if (Vector3.Distance(transform.position, target.position) <= 10f)
    //    {
    //        //StateMachine을 근거리로 변경
    //        ChangeState(State.Melee);
    //    }
    //    if (Vector3.Distance(transform.position, target.position) >= 20f)
    //    {
    //        //StateMachine을 추적으로 변경
    //        isLook = false;
    //        noTrace = false;
    //        ChangeState(State.Trace);
    //    }
    //    else
    //    {
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    //IEnumerator KILLED()
    //{
    //    yield return null;
    //}

    //void ChangeState(State newState)
    //{
    //    state = newState;
    //}

    void Update()
    {
        //fireDelay += Time.deltaTime;
        //isFireReady = rate < fireDelay;

        //if (isDead)
        //{
        //    StopAllCoroutines();
        //}

        //if (target == null) return;
        //// target 이 null 이 아니면 target 을 계속 추적
        //if (!noTrace)
        //{
        //    nav.isStopped = false;
        //    nav.SetDestination(target.position);
        //}
        //else
        //{
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        //if (!isDead)
        //{
        //    lookVec = new Vector3(h, 0, v) * 5f;
        //    transform.LookAt(target.position);
        //}
            
            
            
        //    nav.isStopped = true;

        //}

        

        //if (curHealth > 0)
        //{
        //    if (Vector3.Distance(transform.position, target.position) <= 4f && !isAttacking)
        //    {
        //        Attack();
        //    }
        //    else
        //    {
        //        if (isHitting && isAttacking)
        //        {
        //            nav.isStopped = true;
        //        }
        //        else
        //        {
        //            nav.destination = target.position;
        //            nav.isStopped = false;
        //        }

        //        anim_boss.SetBool("Running", true);
        //    }
        //}
        //else
        //{
        //    anim_boss.SetBool("Running", false);
        //}

    }

    //void FreezeVelocity()
    //{
    //    rigid.velocity = Vector3.zero;
    //    rigid.angularVelocity = Vector3.zero;
    //}

    //void FixedUpdate()
    //{
    //    FreezeVelocity();
    //}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            EnemyCanvasGo.GetComponent<EnemyHpBar>().Boss_Dmg(weapon.damage);
            StartCoroutine(OnDamage());
            Debug.Log("Melee : " + curHealth);
        }
    }
    
    //IEnumerator CheckState()
    //{
    //    while (!isDead)
    //    {
    //        yield return new WaitForSeconds(0.2f);

    //        float dist = Vector3.Distance(transform.position, target.position);

    //        if(dist <= attackDist)
    //        {
    //            currentState = curState.attack;
    //        }
    //        else if (dist <= traceDist)
    //        {
    //            currentState = curState.trace;
    //        }
    //        else
    //        {
    //            currentState = curState.idle;
    //        }
    //    }
    //}

    //IEnumerator CheckStateForAction()
    //{
    //    while (!isDead)
    //    {
    //        switch (curState)
    //        {
    //            case curState.idle:
    //                nav.Stop();
    //                break;
    //            case curState.trace:
    //                nav.destination = target.position;
    //                nav.Resume();
    //                break;
    //            case curState.attack:
    //                break;
    //        }

    //        yield return null;
    //    }
    //}

    IEnumerator OnDamage()
    {
        //mat.color = Color.red;
        if (!isAttacking)
        {
            anim_boss.SetTrigger("Hitting");
            isHitting = true;
            yield return new WaitForSeconds(0.2f);
            isHitting = false;

        }
         
        if (curHealth > 0)
        {
            //mat.color = Color.white;
        }

        else
        {
            //mat.color = Color.gray;
            anim_boss.SetTrigger("Yoodahee");
            target = null;
            isDead = true;
            gameObject.layer = 12;
            Destroy(gameObject, 4);
            // 다른 씬으로 이동
            SceneManager.LoadScene("FirstStage");

        }
    }

    //public void StartMeleeThink()
    //{
    //    StartCoroutine("MeleeThink");
    //}

    //IEnumerator MeleeThink()
    //{
    //    if (isFireReady)
    //    {
            
    //        fireDelay = 0f;
    //        yield return new WaitForSeconds(0.1f);

    //        int ranAction = Random.Range(0, 3);

    //        switch (ranAction)
    //        {
    //            case 0:
                    
    //                StopCoroutine("Wing");
    //                StartCoroutine("Wing");
    //                isFireReady = false;
    //                break;

    //            case 1:
    //                StopCoroutine("Wing");
    //                StartCoroutine("Wing");
    //                isFireReady = false;
    //                break;

    //            case 2:
    //                anim_boss.SetTrigger("Kicking");
    //                isAttacking = true;
    //                StopCoroutine("Kick");
    //                StartCoroutine("Kick");
    //                isFireReady = false;
    //                break;
    //        }
    //    }
    //}

    //public void StartRangeThink()
    //{
    //    StartCoroutine("RangeThink");
    //}

    //IEnumerator RangeThink()
    //{
    //    //boxCollider.enabled = false;
    //    if (isFireReady)
    //    {
    //        fireDelay = 0f;
    //        rate = 6f;
    //        isLook = false;
    //        isAttacking = true;
    //        //noTrace = true;
    //        anim_boss.SetTrigger("Dashing");
    //        ps.Play();
    //        yield return new WaitForSeconds(0.5f);
    //        ps.Stop();
    //        dashVec = transform.forward;
    //        yield return new WaitForSeconds(1f);
    //        rigid.AddForce(dashVec * 20, ForceMode.Impulse);
    //        meleeArea.enabled = false;
    //        int dotCount = 10;
    //        for(int i = 0; i < dotCount; i ++)
    //        {
    //            meleeArea.enabled = !meleeArea;

    //            yield return new WaitForSeconds(0.2f);
    //        }

    //        yield return new WaitForSeconds(0.1f);
    //        rigid.AddForce(-rigid.velocity, ForceMode.VelocityChange);
    //        meleeArea.enabled = false;
    //        yield return new WaitForSeconds(1f);
    //        isAttacking = false;
    //        //noTrace = false;
    //        isLook = true;
            
    //    }
        //boxCollider.enabled = true;
        //noTrace = false;
        //ChangeState(State.Trace);


        //int ranAction = Random.Range(0, 2);

        //switch (ranAction)
        //{
        //    case 0:
        //        anim_boss.SetTrigger("AxeSlash");
        //        isAttacking = true;
        //        StopCoroutine("Axe");
        //        StartCoroutine("Axe");
        //        isFireReady = false;
        //        break;

        //    case 1:
        //        anim_boss.SetTrigger("Kicking");
        //        isAttacking = true;
        //        StopCoroutine("Kick");
        //        StartCoroutine("Kick");
        //        isFireReady = false;
        //        break;
        //}
    //}

    //void Attack()
    //{
    //    fireDelay += Time.deltaTime;
    //    isFireReady = rate < fireDelay;

    //    if (isFireReady)
    //    {
    //        StopCoroutine("Think");
    //        StartCoroutine("Think");
    //    }
        
    //}

    //IEnumerator Wing()
    //{
    //    anim_boss.SetTrigger("WingSlash");
    //    isLook = false;
    //    isAttacking = true;
    //    yield return new WaitForSeconds(0.5f);
    //    meleeArea.enabled = true;
       
    //    noTrace = true;
    //    rate = 3f;
    //    //1
    //    //trailEffect.enabled = true;

    //    //2
    //    yield return new WaitForSeconds(0.3f);
    //    meleeArea.enabled = false;
    //    isAttacking = false;
    //    isLook = true;
    //}
    //IEnumerator Kick()
    //{
    //    noTrace = true;
    //    rate = 3f;
    //    //1
    //    yield return new WaitForSeconds(0.15f);
    //    isLook = false;
    //    isAttacking = true;
    //    //trailEffect.enabled = true;

    //    yield return new WaitForSeconds(0.95f);
    //    meleeArea.enabled = true;

    //    yield return new WaitForSeconds(0.2f);
    //    meleeArea.enabled = false;
    //    isAttacking = false;
    //    isLook = true;
    //}

}




