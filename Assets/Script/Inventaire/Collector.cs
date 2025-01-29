using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
         Icollectible collectible = collision.gameObject.GetComponent<Icollectible>();
        if (collectible != null)
        {
            collectible.Collect();
        }
    }
}
