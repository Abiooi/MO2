using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    public Slider hpSlider;
    public Slider backHpSlider;
    public bool backHpHit = false;


    public float maxHp = 5000f;
    public float currentHp = 5000f;

    void Start()
    {

    }

    void Update()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 5f);
        if (backHpHit)
        {
            backHpSlider.value = Mathf.Lerp(backHpSlider.value, hpSlider.value, Time.deltaTime * 10f);
            if (hpSlider.value >= backHpSlider.value - 0.01f)
            {
                backHpHit = false;
                backHpSlider.value = hpSlider.value;
            }
        }
    }

    public void Boss_Dmg(float damage)
    {
        currentHp -= damage;
        Invoke("Boss_BackHpFun", 0.5f);
    }
    void Boss_BackHpFun()
    {
        backHpHit = true;
    }
}
