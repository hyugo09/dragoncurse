using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }
    public void Playgame()
    {
        GameManager.Instance.GoToScene("Tuto");
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    

}
