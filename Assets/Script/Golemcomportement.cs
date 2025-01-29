using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Golemcomportement : MonoBehaviour
{
    public float speed;
    public float dmg = 2;
    public float lineofsight;
    public float shotingrange;
    public float meleerange;
    public float firerate = 6f;
    public float nextfiretime;
    public float meleetime;
    public float meleerate = 2;
    public bool change = false;
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
        healhsystem = GetComponent<healhsystem>();
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
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            change = false;
        }
        float distancefromplayer =  Vector2.Distance(player.position, transform.position);
        if (distancefromplayer < lineofsight && distancefromplayer > shotingrange)
        {
            
           
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distancefromplayer <= shotingrange && nextfiretime < Time.time)
        {
            if (nextfiretime > Time.time)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            }
            animation.SetTrigger("shoot");
            
           GameObject newBullet = Instantiate(bullet, bulletparent.transform.position, Quaternion.identity);
           newBullet.transform.position = bulletparent.transform.position;
            nextfiretime = Time.time+ firerate;
            
            
        }

        else if (distancefromplayer <= meleerange && meleetime < Time.time)
        {
            animation.SetTrigger("melee");
            Collider2D[] hitennemies = Physics2D.OverlapBoxAll(attackpoint.position, hitbox, layersplayer);

            foreach (Collider2D hit in hitennemies)
            {
                PlayerHealth playerdmg = hit.gameObject.GetComponent<PlayerHealth>();
                if (playerdmg != null)
                {
                    
                    playerdmg.TakeDamage(dmg);
                    Debug.Log("j pris des dmg");
                }


            }
            meleetime = meleerate + Time.time;
        }


        
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centre.transform.position, lineofsight);
        Gizmos.DrawWireSphere(centre.transform.position, shotingrange);
        Gizmos.DrawWireSphere(centre.transform.position, meleerange);
        if (attackpoint == null)
            return;
        Gizmos.DrawWireCube(attackpoint.position, hitbox);
    }
    

   

    



}
