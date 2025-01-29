using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class FirewormComportement : MonoBehaviour
{
    public float speed;
    public float dmg;
    public float lineofsight;
    public float shotingrange;
    public bool change = false;
    public float firerate = 6f;
    public float nextfiretime;
    private healhsystem healhsystem;
    public GameObject bullet;
    public GameObject bulletparent;
    public Transform attackpoint;
    public Vector3 hitbox;
    private Transform player;
    public GameObject centre;
    public Animator animation;
    private SpriteRenderer sprite;
    public LayerMask layersplayer;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animation = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    void Update()
    {
        
        if (player.transform.position.x > this.gameObject.transform.position.x && change == false)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                change = true;
        }
        else if (player.transform.position.x < this.gameObject.transform.position.x && change == true)
         { transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            change=false;
        }

        float distancefromplayer = Vector2.Distance(player.position, transform.position);
        if (distancefromplayer < lineofsight && distancefromplayer > shotingrange)
        {
            animation.SetBool("ismoving", true);

            
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distancefromplayer <= shotingrange && nextfiretime < Time.time)
        {
            animation.SetBool("ismoving", false);
            animation.SetTrigger("shoot");
            
            GameObject newBullet = Instantiate(bullet, bulletparent.transform.position, Quaternion.identity);
            newBullet.transform.position = bulletparent.transform.position;
            nextfiretime = Time.time + firerate;
            

        }


        
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, lineofsight);
        Gizmos.DrawWireSphere(this.gameObject.transform.position, shotingrange);
       
        if (attackpoint == null)
            return;
        Gizmos.DrawWireCube(attackpoint.position, hitbox);
    }
}
