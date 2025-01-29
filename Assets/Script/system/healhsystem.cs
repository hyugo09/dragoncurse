using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class healhsystem : MonoBehaviour
{
    public int currenthealht = 0;
    public int maxhealth;
    private Animator anim;
    [SerializeField] private ParticleSystem ps;
    GameObject psGO;

    [SerializeField] private Tilemap map;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currenthealht = maxhealth;

        psGO = GameObject.Find("EnnemyHitParticle");

        ps = psGO.GetComponent<ParticleSystem>();


        map = GameObject.Find("Tilemap").GetComponent<Tilemap>();


    }

    // Update is called once per frame
    public void TakeDamage(int damageAmount)
    {
        currenthealht -= damageAmount;
        currenthealht = Mathf.Clamp(currenthealht, 0, maxhealth);

        if (currenthealht <= 0)
        {
            if (this.gameObject.CompareTag("destructible"))
            {
                map.SetTile(map.WorldToCell(this.transform.position), null);
                map.SetTile(map.WorldToCell(this.transform.position + Vector3.down), null);
                this.gameObject.SetActive(false);
            }

            

            //anim.SetTrigger("dead");
            //Destroy(this.gameObject, 2);
            if (this.gameObject.tag.Equals("ennemy"))
            {
                anim.SetTrigger("dead");
                Destroy(this.gameObject, 2);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        if(!this.gameObject.CompareTag("destructible"))
        {
            psGO = GameObject.Find("EnnemyHitParticle");

            if (this.gameObject.name == "Truegolem")
            {
                Golemcomportement gc = GetComponent<Golemcomportement>();
                psGO.GetComponent<CameraFollow>().cible = gc.centre;
            }
            else if (this.gameObject.name == "FireWorm")
            {
                FirewormComportement cp = GetComponent<FirewormComportement>();
                psGO.GetComponent<CameraFollow>().cible = cp.centre;
            }

            GameObject player = GameObject.Find("player");
            if (player.transform.position.x > this.gameObject.transform.position.x)
            {
                psGO.transform.rotation = Quaternion.Euler(psGO.transform.rotation.x, psGO.transform.rotation.y, 90);
            }
            else if (player.transform.position.x < this.gameObject.transform.position.x)
            {
                psGO.transform.rotation = Quaternion.Euler(psGO.transform.rotation.x, psGO.transform.rotation.y, 0);
            }

            ps.Play();
        }
        
    }

    public void GetHealth(int healthamout)
    {
        currenthealht += healthamout;
        currenthealht = Mathf.Clamp(currenthealht, 0, maxhealth);
    }

    public bool IsDead()
    {
        if (currenthealht > 0)
        {
            return false;
        }
        else if (currenthealht <= 0)
        {
            anim.SetTrigger("dead");
            Destroy(this.gameObject, 3);
            return true;
        }
        else
        {
            return false;
        }
    }

    

}
