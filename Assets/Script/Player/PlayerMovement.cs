using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // variable attaque
    public Transform attackpoint;
    public float attackrange;
    public Vector3 hitboxrange;
    public LayerMask ennemiesLayers;
    int dommage = 2;


    public bool ismoving = false;
    // vitesse du joueur
    public float speed = 5;

    // variable pour le projectile
    public GameObject projectilePrefabNormal;
    public GameObject projectilePrefabVert;
    public GameObject projectilePrefabMauve;
    public GameObject ProjectileOffset;

    // variable pour le saut

    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private float jumpTime = 0;
    private float jumpSpeed = 13;
    private float maxJumpTime = 0.4f;

    private Rigidbody2D rb;
    public bool isGrounded;
    public bool wall;
    private bool isJumping = false;
    private bool UsedDoubleJump = false;
    

    // variable pour le dash
    public float dashDistance = 4f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 0.5f;

    private bool isDashing = false;
    private float dashTime = 1;

    private float DashCancelForceX = 50f;
    private float DashCancelForceY = 1f;

    //bounce related variable
    float bounceTime = 0;
    internal bool isBoucing = false;

    //pour le sprite
    SpriteRenderer sr;

    ////truc du pool pas utiliser
    //private int poolSize = 5;
    //Queue<GameObject> projectileQueue = new Queue<GameObject>();

    //private void Awake()
    //{
    //    AddProjectileToPool();
    //}

    //private void AddProjectileToPool()
    //{
    //    for (int i = 0; i < poolSize; i++)
    //    {
    //        GameObject projectile = Instantiate(projectilePrefav);
    //        projectileQueue.Enqueue(projectile);
    //        projectile.SetActive(false);
    //    }
    //}
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
    }
    //enum
    enum direction
    {
        droite,
        gauche
    }
    direction LastDirection;
    
    enum type_projectile
    {
        normal,
        vert,
        mauve
    }
    type_projectile TypeP = type_projectile.normal;

    private void Update()
    {
        //truc pour le saut
        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapArea(
            new Vector2(transform.position.x - 0.5f, transform.position.y - 1.2f),
            new Vector2(transform.position.x + 0.5f, transform.position.y - 1.25f),
            groundLayer);
        //chek if there is a wall
        wall = Physics2D.OverlapArea(
            new Vector2(transform.position.x - 0.5f, transform.position.y - 1.2f),
            new Vector2(transform.position.x + 0.5f, transform.position.y - 1.25f),
            wallLayer);

        //reinitiaize le double saut quand le joueur touche le sol
        if (isGrounded)
        {
            UsedDoubleJump = false;
        }

        //calcule le temp qui c passe depuis le dernier dash
        dashTime += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (isJumping)
        {
            if (jumpTime < maxJumpTime)
            {
                float jumpSpeedThisFrame = jumpSpeed * (1f - jumpTime / maxJumpTime);
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeedThisFrame);
                jumpTime += Time.fixedDeltaTime;
            }
            else
            {
                DeactivateJump();
            }
           
        }

        if (isBoucing)
        {
            bounceTime += Time.fixedDeltaTime;
            if (bounceTime >= 1)
            {
                Debug.Log("boucing = false");
                isBoucing = false;
                bounceTime = 0;
            }

        }

    }

    internal void ActivateJump()
    {
        if (isGrounded && !isJumping)
        {
            isJumping = true;
            jumpTime = 0f;

        }

        if (!isGrounded && UsedDoubleJump == false)
        {
            isJumping = true;
            jumpTime = 0f;
            UsedDoubleJump = true;


            // trigger aniation jump




        }

    }
    internal void DeactivateJump()
    {
        if (isJumping)
        {
            isJumping = false;
        }
    }

    internal void Move(Vector2 Direction)
    {
        //transform.Translate(Direction * speed * Time.deltaTime);
        if (!isBoucing || !isDashing)
        {
            rb.velocity = new Vector2(Direction.x * speed, rb.velocity.y);
        }
        else if (isGrounded)
        {
            rb.velocity += new Vector2(Direction.x / 100, 0);
        }

        if (Direction == Vector2.right)
        {
            //change seulement si il n'allait pas deja a droite
            if (LastDirection != direction.droite)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                //sr.flipX = false;
            }
            //enregistre la derniere direction droite
            LastDirection = direction.droite;


        }
        else
        {
            //change seulement si il n'allait pas deja a gauche
            if (LastDirection != direction.gauche) { transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y); }
           // sr.flipX = true;
            //enregistre la derniere direction gauche
            LastDirection = direction.gauche;

        }
    }

    internal void StopMove()
    {
        if (!isBoucing)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    // gere l attaque et l animation de l attaque

    internal void Attack()
    {

        Collider2D[] hitennemies = Physics2D.OverlapBoxAll(attackpoint.position, hitboxrange, ennemiesLayers);
		
        //je suis rendu la
       CameraShake.instance.Shake(5,5);

        
        foreach (Collider2D hit in hitennemies)
        {
            healhsystem monsterdmg = hit.gameObject.GetComponent<healhsystem>();
            if (monsterdmg != null)
            {
                monsterdmg.TakeDamage(dommage);
                Debug.Log("se chien a prit de dommage");
            }
            if (hit.tag.Equals("vert"))
            {
               // hit.attachedRigidbody.AddForce(Vector2)
            }
            

        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackpoint == null)
            return;
        Gizmos.DrawWireCube(attackpoint.position, hitboxrange);
    }

    internal void ActivateDash()
    {
        if (!isDashing && dashTime > dashCooldown)
        {
            if (LastDirection == direction.droite)
            {
                // dash a droite
                StartCoroutine(PerformDash(transform.position + transform.right * dashDistance));
                //reset le temp
                dashTime = 0;
            }
            else
            {
                //dash a gauche
                StartCoroutine(PerformDash(transform.position + -transform.right * dashDistance));
                //reset le temp
                dashTime = 0;
            }

        }

    }


    internal IEnumerator PerformDash(Vector3 dashTarget)
    {
        //take the origianl gravity
        float originalGravity = rb.gravityScale;
        //make the player unafected by gravity
        //(pour regler bug ou apres dash la gravite etait accumule) 
        rb.gravityScale = 0;
        isDashing = true;
        Vector3 originalPosition = transform.position;


        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            if (isJumping)
            {
                if (LastDirection == direction.droite)
                {
                    rb.AddForce(new Vector2(DashCancelForceX, DashCancelForceY));

                }
                else if (LastDirection == direction.gauche)
                {
                    rb.AddForce(new Vector2(-DashCancelForceX, DashCancelForceY));
     
                }

                // avoir le meme effet de stun que le bounce
                isBoucing = true;
                //desactive le dash
                isDashing = false;
                //remet lagravite
                rb.gravityScale = originalGravity;
                //brak la coroutine
                yield break;
            }
            if (wall)
            {
                isDashing = false;
                rb.gravityScale = originalGravity;
                yield break;
            }

            // va de la posistion originale a la fin du dash dans le temp donne
            transform.position = Vector3.Lerp(originalPosition, dashTarget, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //s'assure que le perso est bien au bon endroit a la fin du dash i guess
        //transform.position = dashTarget;
        isDashing = false;
        //restart the gravity to the point it was
        rb.gravityScale = originalGravity;
        //reset la velosity du saut
        //(pour pas que la veocite garder pas le saut donne un mini boost apres le dash)
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    //pour changer le type de projectile lance
    internal void ChangeProjectile()
    {
        if(TypeP + 1 < (type_projectile)3)
        {
            TypeP++;
        }
        else
        {
            TypeP = 0;
        }
    }

    internal void LancerProjectile()
    {
        // a change mais gameryan va le faire... tu vas le faire en ?
        //Instantiate(projectilePrefav, ProjectileOffset.transform);

        Vector3 gauchedirection = Vector3.zero;

        if (transform.localScale.x == -1)
        {
            gauchedirection.z = 100;
        }

        Quaternion gaucheQuaternion = Quaternion.Euler(gauchedirection);

        //pool system mais on l'utilise pas
        //if (projectileQueue.Count == 0)
        //{
        //    AddProjectileToPool();
        //}

        //GameObject projectile = projectileQueue.Dequeue();
        //projectile.SetActive(true);
        //projectile.transform.position = ProjectileOffset.transform.position;
        //projectile.transform.rotation = gaucheQuaternion;


        // sa a changer AHAHAHA!
        //envoie le proejctile selon le type/couleur
        if(TypeP == type_projectile.normal)
        {
            Instantiate(projectilePrefabNormal, ProjectileOffset.transform.position, Quaternion.identity);
        }
        else if (TypeP == type_projectile.vert)
        {
            Instantiate(projectilePrefabVert, ProjectileOffset.transform.position, Quaternion.identity);
        }
        else if (TypeP == type_projectile.mauve)
        {
            Instantiate(projectilePrefabMauve, ProjectileOffset.transform.position, Quaternion.identity);
        }
    }

    //pour la collision avec un collectible
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //collectible
        if (collision.gameObject.CompareTag("collectible"))
        {
            Debug.Log("collectible attrapper");
            Destroy(collision.gameObject);
        }
    }
}
