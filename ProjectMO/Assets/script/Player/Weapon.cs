using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public float damage;
    public float rate;
    public BoxCollider meleeArea;
    public Camera followCamera;

    public Animator anim;

    Vector3 cameraOriginalPos;

    //float delay = 0.5f;

    public void Use()
    {
        if (type == Type.Melee)
        {
            
            StopCoroutine("Swing");
            StartCoroutine("Swing");
            
        }
    }
    IEnumerator Swing()
    {
        //1
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        

        //2
        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;
        
        //3
        yield return new WaitForSeconds(0.3f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "EnemyDmg")
        {
            //cameraOriginalPos = followCamera.transform.position;
            //StartCoroutine(CameraShake(0.01f, 0.001f));
            StartCoroutine("HitStop");
        }
    }

    IEnumerator HitStop()
    {
        anim.speed *= 0.1f;
        yield return new WaitForSeconds(0.15f);
        anim.speed *= 20f;
        yield return new WaitForSeconds(0.15f);
        anim.speed *= 0.5f;
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
}
