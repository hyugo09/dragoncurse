using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public static PlayerHealth instance;

    public int maxHealth;
    [SerializeField] float health;
    
    public event Action DamageTaken;
    public event Action HealthUpgraded;

    public float Health
    {
        get
        {
            return health;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
        {
            
            instance = this;
        }
    }

    private void Start()
    {
        
        
    }

    public void TakeDamage( float damage)
    {
        if(health <= 0)
        {
            GameManager.Instance.Gameover();
            
        }
        health -= damage;
        if(DamageTaken != null)
        {
            DamageTaken();
        }
    }

    public void Heal(float heal)
    {
        if (health >= maxHealth + heal)
        {
            health = maxHealth;
            //return;
        }
        else
        {
            health += heal;
        }
        
        if (DamageTaken != null)
        {
            DamageTaken();
        }
    }

    public void UpgradeHealth()
    {
        maxHealth++;
        health = maxHealth;
        if(HealthUpgraded != null)
        {
            HealthUpgraded();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("collectible"))
        {
            TakeDamage(0.5f);
            Debug.Log("take damage");
        }
        
    }

}
