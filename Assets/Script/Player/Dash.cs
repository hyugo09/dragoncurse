using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 1f;

    private bool isDashing = false;
    private float dashTime = 1;

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //calcule le temp qui c passe depuis le dernier dash
        dashTime += Time.deltaTime;
        //si shift droit est appuye, n'est pas dans un dash, que le temp depuis le dernier dash
        //est plus grang que le cooldown et que la droite est appuye
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashTime>dashCooldown && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {

            // dash a droite
            StartCoroutine(PerformDash(transform.position + transform.right * dashDistance));
            //reset le temp
            dashTime= 0;
            
        }
        //meme chose que l'autre mais la gauche est appuye
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashTime > dashCooldown && (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))) 
        {
            //dash a gauche
            StartCoroutine(PerformDash(transform.position + -transform.right * dashDistance));
            //reset le temp
            dashTime= 0;
        }
    }

    IEnumerator PerformDash(Vector3 dashTarget)
    {
        //take the origianl gravity
        float originalGravity= rb.gravityScale;
        //make the player unafected by gravity
        //(pour regler bug ou apres dash la gravite etait accumule) 
        rb.gravityScale = 0;
        isDashing = true;
        Vector3 originalPosition = transform.position;
        

        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
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
}


