using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider hpSlider;
    public Slider backHpSlider;
    public bool backHpHit = false;


    public float maxHp = 2000f;
    public float currentHp = 2000f;
    void Start()
    {
        
    }

    void Update()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 5f);
        if (backHpHit)
        {
            backHpSlider.value = Mathf.Lerp(backHpSlider.value, hpSlider.value, Time.deltaTime * 10f);
            if(hpSlider.value >= backHpSlider.value - 0.01f)
            {
                backHpHit = false;
                backHpSlider.value = hpSlider.value;
            }
        }
    }

    public void Dmg()
    {
        currentHp -= 300f;
        Invoke("BackHpFun", 0.5f);
    }
    public void Potion(int divine)
    {

        if (currentHp < 2000)
        {
            currentHp += divine;
            Invoke("BackHpFun", 0.5f);
        }

    }
    void BackHpFun()
    {
        backHpHit = true;
        
    }
}
