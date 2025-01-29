using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class PlayerControl : MonoBehaviour
{
    //input 
    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode leftKey1 = KeyCode.LeftArrow;
    private KeyCode leftKey2 = KeyCode.A;
    private KeyCode rightKey1 = KeyCode.RightArrow;
    private KeyCode rightKey2 = KeyCode.D;
    private KeyCode AttackKey1 = KeyCode.Z;
    private KeyCode AttackKey2 = KeyCode.Mouse0;
    private KeyCode ProjectileKey1 = KeyCode.X;
    private KeyCode ProjectileKey2 = KeyCode.Mouse1;
    private KeyCode DashKey = KeyCode.LeftShift;

    //other script
    public PlayerMovement playerMovement;
    public animationscript animationscript;


   

    // Update is called once per frame
    void Update()
    {
        //fait les trucs du joueur suelement quand le jeu est en mode play
        if (GameManager.Instance.gameMode == GameMode.PLAY)
        {


            if (Input.GetKeyDown(AttackKey1) || Input.GetKeyDown(AttackKey2))
            {

                animationscript.attack();
                //AudioManager.instance.PlaySong("fireball");
                playerMovement.Attack();
                AudioManager.instance.PlaySong("");
            }




            //controle le joueur pour aller a droite et a gauche
            if (Input.GetKey(rightKey1) || Input.GetKey(rightKey2))
            {
                playerMovement.Move(Vector2.right);
                // set animation sur true
                animationscript.animouvementactive();
            }

            if (Input.GetKey(leftKey1) || Input.GetKey(leftKey2))
            {
                playerMovement.Move(Vector2.left);
                animationscript.animouvementactive();
            }
            // deactive l animation des mouvement
            if ((Input.GetKeyUp(rightKey1) || Input.GetKeyUp(rightKey2)) || (Input.GetKeyUp(leftKey1) || Input.GetKeyUp(leftKey2)))
            {
                animationscript.animouvementdeactive();
                playerMovement.StopMove();
            }





            // lance le projectile
            if (Input.GetKeyDown(ProjectileKey1) || Input.GetKeyDown(ProjectileKey2))
            {
                playerMovement.LancerProjectile();
               
            }

            // ouvre un menu (pour l<instant c le main mais bon)
            if (Input.GetKeyDown(KeyCode.M))
            {

                GameManager.Instance.GoToMainMenu();

            }


            // saut
            if (Input.GetKeyDown(jumpKey))
            {
                playerMovement.ActivateJump();
                animationscript.AniJump();
            }



            //pour quand espace est relache au millieu du saut
            if (Input.GetKeyUp(jumpKey))
            {
                playerMovement.DeactivateJump();
            }

            //pour le dash
            if (Input.GetKeyDown(DashKey))
            {
                playerMovement.ActivateDash();
            }

            //pause
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameManager.Instance.gameMode == GameMode.PLAY)
                {
                    GameManager.Instance.Pause();
                }
                else
                {
                    GameManager.Instance.Resume();
                }
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                playerMovement.ChangeProjectile();
            }

        }

    }
}

