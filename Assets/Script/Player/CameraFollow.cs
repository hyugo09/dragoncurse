using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [ SerializeField ]private float zoffset= -10;
    public GameObject cible;
    // Start is called before the first frame update
    void Start()
    {
        zoffset = transform.position.z;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    void LateUpdate()
    {

        if (!cible) return;
        else
        {
            Follow();
        }


    }

    private void Follow()
    {
        Vector3 PositionCibleEtape;

        PositionCibleEtape = Vector3.Lerp(this.transform.position, new Vector3(cible.transform.position.x,cible.transform.position.y, zoffset), 1f);
        transform.position = PositionCibleEtape;
    }
}
