using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mo : MonoBehaviour
{
    public GameObject playerCanvasGo;
    public Camera followCamera;
    public ParticleSystem ps;
    public PlayerLotate playerLotate;

    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    float hAxis;
    float vAxis;

    bool isComboResetting = false;
    float comboResetDelay = 2f;

    bool fDown;
    bool dDown;
    bool iDown;
    bool hDown;
    bool sDown;
    bool isDodge;
    bool isDash;
    bool isSide;
    bool isDead;
    bool isFireReady = true;
    bool isAttack;
    bool animEnd;

    Vector3 sideVec;
    Vector3 moveVec;
    Vector3 dodgeVec;
    Vector3 dashVec;
    Vector3 attackVec;
    Vector3 cameraOriginalPos;

    int ComboCount;
    int OriginalComboCount = 0;
    float fireDelay;
    float lastFDownTime;
    float comboResetTime = 2.0f;

    public int maxHealth;
    public int curHealth;

    Animator anim;
    CameraControl cam;

    int G_power = 800; 

    GameObject nearObject;
    Weapon  equipWeapon;
    int equipWeaponIndex;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        cam = GetComponent<CameraControl>();
        playerLotate = GetComponent<PlayerLotate>();
    }

    void Start()
    {
        ps.Stop();
        anim.speed *= 1.5f;
        
    }

    void Update()
    {

        GetInput();
 
        //Turn();

        Attack();

        Dodge();

        Dash();

        Interation();

        Heal();

        if(Time.time - lastFDownTime > comboResetTime)
        {
            ResetCombo();
        }

    }

    void LateUpdate()
    {
        Move();
    }
    //void FixedUpdate()
    //{
    //    if (!isAttack && !isDead)
    //    {
    //        transform.forward = cam.transform.forward;
    //    }
    //}

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        dDown = Input.GetButtonDown("Dodge");
        iDown = Input.GetButtonDown("Interation");
        fDown = Input.GetButtonDown("Fire1");
        hDown = Input.GetButtonDown("Heal");
        sDown = Input.GetButtonDown("MaskSkill");
       
    }

    void Move()
    {
        //Vector3 forward = playerLotate.transform.TransformDirection(Vector3.forward);
        //Vector3 right = playerLotate.transform.TransformDirection(Vector3.right);

        Vector3 localVec = new Vector3(hAxis, 0, vAxis);

        // 로컬 좌표계에서의 이동 벡터를 전역 좌표계로 변환
        moveVec = playerLotate.transform.TransformDirection(localVec);


        if (isDodge)
        {
            moveVec = dodgeVec;
        }
        if (isDash)
        {
            moveVec = dashVec;
        }

        if (isSide && moveVec == sideVec || !isFireReady || isDead)
        {
            moveVec = Vector3.zero;
        }

        // 플레이어 이동
        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
    }

    //void Turn()
    //{

    //    //마우스 회전
    //    if (IsDown())
    //    {

    //        Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit rayHit;
    //        if (Physics.Raycast(ray, out rayHit, 100))
    //        {
    //            Vector3 nextVec = rayHit.point - transform.position;
    //            nextVec.y = 0;  // 부피가 큰 적을 대상으로 할때, y축으로 기울어지는 것을 방지
    //            transform.LookAt(transform.position + nextVec);
    //        }
    //    }
    //}

    bool IsDown()
    {
        return (fDown && !isDead);
    }

    void Attack()
    {
        if (equipWeapon == null)
        {
            return;
        }

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(CanAttack())
        {
            lastFDownTime = Time.time;

            isAttack = true;
            if (ComboCount == 3)
            {
                ComboCount = 0;
            }

            equipWeapon.Use();
            anim.SetTrigger("doSwing");
            ComboCount++;
            OriginalComboCount = ComboCount;
            anim.SetInteger("attackCombo", ComboCount);
            fireDelay = 0;
            Invoke("AttackOut", 0.5f);
        }
    }

    bool CanAttack()
    {
        return (IsDown() && isFireReady && !isDodge);
    }
   
    void ResetCombo()
    {
        ComboCount = 0;
        anim.SetTrigger("animEnd");
    }
    void AttackOut()
    {
        isAttack = false;
    }

    //IEnumerator ComboCheck()
    //{
    //    yield return new WaitForSeconds(3f);
        
    //    ComboCount = 0;
    //    anim.SetTrigger("animEnd");
    //    anim.ResetTrigger("enimEnd");

    //}

    //void Attack()
    //{
    //    fireDelay += Time.deltaTime;
    //    isFireReady = rate < fireDelay;

    //    if (fDown && isFireReady && !isDodge)
    //    {
    //        if(ComboCount > 2)
    //        {
    //            ComboCount = 0;
    //        }
    //        anim.SetTrigger("doSwing");
    //        anim.SetInteger("attackCombo", ComboCount);
    //        ComboCount++;
    //        fireDelay = 0; 
    //    }
    //}

    void Dash()
    {
        if(sDown && !isDash && !isDead && !isDodge)
        {
            

            dashVec = moveVec;

            speed *= 10f;
            anim.SetTrigger("doDash");
            isDash = true;

            Invoke("DashOut", 0.2f);
        }
    }
    void DashOut()
    {
        speed /= 10f;
        isDash = false;
    }

    void Dodge()
    {
        if (dDown && !isDodge && !isDead && !isAttack)
        {
            dodgeVec = moveVec;

            speed *= 2f;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.8f);

        }
    }
    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;

    }
    void Interation()
    {
        if (iDown && nearObject != null && !isDodge)
        {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
                equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();  // 교체할 무기가 없는동안 현재무기 저장 변수
                equipWeapon.gameObject.SetActive(true);
                equipWeaponIndex = weaponIndex;
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            if (!isDodge)
            {
                //cameraOriginalPos = followCamera.transform.position;
                //StartCoroutine(CameraShake(0.1f, 0.4f));
                curHealth -= 300;
                playerCanvasGo.GetComponent<HpBar>().Dmg();

                StartCoroutine(OnDamage());
            }
        }
    }

    IEnumerator OnDamage()
    {
        //mat.color = Color.red;
        if (!isAttack && curHealth > 0)
        {
            anim.SetTrigger("doHit");
            yield return new WaitForSeconds(0.1f);
        }

        //if (curHealth > 0)
        //{
        //    curHealth -= 300;
        //    //mat.color = Color.white;
        //}
        if(curHealth < 0)
        {
            //mat.color = Color.gray;
            anim.SetTrigger("daheeyang");
            isDead = true;

            //Destroy(gameObject, 4);
        }
        else
        {
            isDead = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isSide = true;
            sideVec = moveVec;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isSide = false;
            sideVec = Vector3.zero;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon")
        {
            nearObject = other.gameObject;
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = other.gameObject;
        }
    }

    void Heal()
    {
        if (hDown && !isDodge && !isDead && curHealth <= 2000)
        {
            playerCanvasGo.GetComponent<HpBar>().Potion(G_power);
            curHealth += G_power;
            ps.Play();
            Invoke("HealEnd", 1.0f);
        }
    }
    void HealEnd()
    {
        ps.Stop();
    }

    //IEnumerator CameraShake(float duration, float magnitude)
    //{
    //    float timer = 0;

    //    while (timer <= duration)
    //    {
    //        followCamera.transform.localPosition = Random.insideUnitSphere * magnitude + cameraOriginalPos;

    //        timer += Time.deltaTime;
    //        yield return null;
    //    }
    //    followCamera.transform.localPosition = cameraOriginalPos;
    //}



    //void ComboAttack()
    //{
    //    isAttack = false;
    //    StartCoroutine(CheckComboAttack());

    //    IEnumerator CheckComboAttack()
    //    {
    //        if (Input.GetButtonDown("Fire1"))
    //        {
    //            anim.ResetTrigger("enimEnd");
    //            isAttack = true;
    //            yield return new WaitForSeconds(1f);
    //            EndComboAttack();
    //        }
    //    }
    //}

    //void EndComboAttack()
    //{
    //    ComboTime = 2f;
    //    while(ComboTime >= 0)
    //    {
    //        if (fDown)
    //        {
    //            if(ComboCount == 3)
    //            {
    //                anim.SetTrigger("enimEnd");
    //                ComboCount = 0;
    //            }
    //            return;
    //        }
    //        ComboTime -= Time.deltaTime;
    //    }
    //    anim.SetTrigger("enimEnd");
    //    ComboCount = 0;
    //}






    //void CheckStartComboAttack()
    // {
    //     isContinueComboAttack = false;

    //     COR_CheckComboAttack = CheckComboAttack();

    //     StartCoroutine(COR_CheckComboAttack);

    //     IEnumerator CheckComboAttack()
    //     {
    //         yield return CoroutineHelper.WaitUntil(() => Input.GetButtonDown("Fire1"));

    //         isContinueComboAttack = true;
    //     }
    // }
    // void CheckEndComboAttack()
    // {
    //     if (!isContinueComboAttack)
    //     {
    //         AttackAnimEnd();
    //     }
    // }

}
