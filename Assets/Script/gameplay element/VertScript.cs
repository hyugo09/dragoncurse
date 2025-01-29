using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class VertScript : MonoBehaviour
{
    //variable
    BounceScript bounce = null;
    Rigidbody2D thisRigidbody;
    [SerializeField] private Transform player;
    [SerializeField] LayerMask layerProjectile;
    int rayon = 5;
    float speed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (player.transform.localScale.x > 0)
        {

            thisRigidbody.velocity = Vector2.right* speed;
        }
        else
        {
            thisRigidbody.velocity = Vector2.left * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] detection = Physics2D.OverlapCircleAll(transform.position, rayon, layerProjectile);

        foreach (Collider2D c in detection)
        {
            if (c.gameObject.CompareTag("mauve"))
            {
               transform.position = Vector2.MoveTowards(transform.position, c.gameObject.transform.position, speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounce = collision.gameObject.GetComponent<BounceScript>();
        if (bounce != null)
        {
            Debug.Log(collision.gameObject.name);
            // Calculate the bounce direction (away from the collision point).
            Vector2 bounceDirection = collision.contacts[0].point - (Vector2)transform.position;

            Vector2 direction = collision.gameObject.transform.position - this.transform.position;

            bounceDirection.Normalize();
            Debug.Log($"{bounceDirection}");


            bounce.Bounce(bounceDirection);
            //thisRigidbody.AddForce(-bounceDirection * bounce.bounceForce);

        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, rayon);
    }
}
