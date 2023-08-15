using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Potion, Weapon };
    public Type type;

    public int value;

    void Update()
    {
        transform.Rotate(new Vector3(1,1,1) * 50 * Time.deltaTime);
    }
}
