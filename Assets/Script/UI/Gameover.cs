using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    
    private void Awake()
        //change pour le re envoyer
    {// copier coller de pausemenu
        GameManager.Instance.GameoverUI = this.gameObject;
        this.gameObject.SetActive(false);
    }

    public void Restart()
    {// reload la scene actuelle
        GameManager.Instance.ReloadScene();
        // GameManager.Instance.GoToScene("Tuto");
    }
}
