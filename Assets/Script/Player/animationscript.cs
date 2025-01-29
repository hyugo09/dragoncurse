using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationscript : MonoBehaviour
{
    // animation variable
    public Animator animation;
    public bool ismoving = false;
    // vitesse du joueur
    public float speed = 5;
    SpriteRenderer sr;
    void Start()
    {
        animation = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

   
    internal void attack()
    {
        // animation attaque

        animation.SetTrigger("attack");


    }
    internal void animouvementactive()
    {
        // set le bool de move a true
        animation.SetBool("move", true);
    }
    internal void animouvementdeactive()
    {
        // set le bool de move a true
        animation.SetBool("move", false);
    }
    
    internal void AniJump()
    {
        animation.SetTrigger("jump");
    }

}
