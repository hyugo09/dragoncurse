using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    // other script
    PlayerMovement pm;
    Rigidbody2D rb;
    //variable
    public float bounceForce = 1000;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       pm = GetComponent<PlayerMovement>();
    }

    public void Bounce(Vector2 direction)
    {
        direction = direction * bounceForce;

        rb.AddForce(direction);

        if (pm != null)
        {
            pm.isBoucing = true;
        }
    }

}
