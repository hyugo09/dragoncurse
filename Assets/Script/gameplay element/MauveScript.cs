using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauveScript : MonoBehaviour
{
    PlayerHealth health = null;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] float cooldown = 0;
    public float damage =0.1f;

    [SerializeField] LayerMask layerProjectile;
    int rayon = 5;
    float speed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        if (player.transform.localScale.x > 0)
        {

            rb.velocity = Vector2.right * speed;
        }
        else
        {
            rb.velocity = Vector2.left * speed;
        }
    }

    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = 0;
        }

        Collider2D[] detection = Physics2D.OverlapCircleAll(transform.position, rayon, layerProjectile);

        foreach (Collider2D c in detection)
        {
            if (c.gameObject.CompareTag("vert"))
            {
                transform.position = Vector2.MoveTowards(transform.position, c.gameObject.transform.position, speed * Time.deltaTime);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //ici, veux faire interaction entre vert et mouve
        //la je le fait
        //quand le vert est dedans il devient plus petit jusqu'a disparaitre et le vert devient plus gros comme ta mere
        if (collision.gameObject.CompareTag("vert") && cooldown == 0)
        {
            collision.gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0);
            collision.gameObject.GetComponent<CircleCollider2D>().radius += 0.005f;
            transform.localScale -= new Vector3(0.1f,0.1f,0);
            this.GetComponent<CircleCollider2D>().radius -= 0.005f;

            if (transform.localScale.x <= Vector3.zero.x)
            {
                Destroy(gameObject);
            }
        }

        health = collision.gameObject.GetComponent<PlayerHealth>();
        if (health != null && cooldown == 0)
        {
            cooldown = 0.1f;
            health.TakeDamage(0.1f);
        }
    }
   
}
