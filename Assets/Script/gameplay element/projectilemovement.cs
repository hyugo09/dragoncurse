using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class projectilemovement : MonoBehaviour
{


    [SerializeField] private float vitesse = 3;
    [SerializeField] private float lifetime = 10;
    [SerializeField] private int degats = 5;
    [SerializeField] private Transform player;
    [SerializeField] private ParticleSystem particule;

    private Rigidbody2D rb;

    public float pspeed = 7;

    private Queue<GameObject> pool;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        if (player.transform.localScale.x > 0)
        {
            
            rb.AddForce(Vector2.right * vitesse);
        }
        else
        {
            rb.AddForce(Vector2.left * vitesse);
        }
       

        Destroy(this.gameObject, lifetime);
    }

   

    // private void OnTriggerEnter2D(Collider2D collision)
    // {

    // healhsystem health = collision.GetComponent<healhsystem>();

    // if (health)
    // {
    //   health.TakeDamage(degats);


    //Creer Particule
    //ParticleSystem particuleGO = Instantiate(particule);
    //particuleGO.transform.position = this.transform.position;

    //CameraEffects.Singleton.ShakeCamera(0.5f, 1);
    //CameraEffects.Singleton.SetCible(collision.transform);

    //Detruire l'effet apres 2 secondes
    // Destroy(particuleGO.gameObject, 2);

    //   Destroy(this.gameObject);
    // }

    // Update is called once per frame





    private void OnTriggerEnter2D(Collider2D collision)

    {
       
           /* collision.gameObject.GetComponent<healhsystem>().TakeDamage(degats);
            AudioManager.instance.PlaySong("Fireball");

            Destroy(gameObject);*/
        
        healhsystem healht = collision.GetComponent<healhsystem>();

        if (healht)
        {
             healht.TakeDamage(degats);
             Destroy(this.gameObject);
        }

    }

  //  public void SetPoolReference(Queue<GameObject> newpool)
 //   {
       // pool = newpool;
  //  }
    private void ReturnToQueu()
    {

    }

}





