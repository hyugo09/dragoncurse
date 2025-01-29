using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsterbasicprojectile : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float dmg;
    Rigidbody2D bulletrb;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position);
        bulletrb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        Vector2 movedir = (target.transform.position - transform.position).normalized*speed;
        bulletrb.velocity = new Vector2 (movedir.x, movedir.y);
        Destroy(this.gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(dmg);
        }
        Destroyanim();
    }

    // Update is called once per frame

    private void Destroyanim()
    {
        Destroy(this.gameObject);
    }

}
