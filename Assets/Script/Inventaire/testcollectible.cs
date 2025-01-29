using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcollectible : MonoBehaviour, Icollectible
{
    public delegate void handleobject(itemdata itemdata);
    public static event handleobject Onthiscollected;
    public itemdata objectdata;
    public void Collect()
    {
        Debug.Log("tu la collecter");
        Destroy(gameObject);
        Onthiscollected?.Invoke(objectdata);
       
    }


}
