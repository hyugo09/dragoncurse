using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy_AI : MonoBehaviour
{
    //G juste fait sa car c t sur le tableau du prof, mais je pense pas que c utile, tete plus tard
    NavMeshAgent navMeshAgent;
    Slider slider;
    Button button;

    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        transform.rotation = Quaternion.Euler(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.speed = 3.5f;

    }
}
