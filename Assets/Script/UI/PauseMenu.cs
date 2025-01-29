using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private void Awake()
    {
        GameManager.Instance.pauseMenuUI = this.gameObject;
        this.gameObject.SetActive(false);
    }
    public void Resume()
    {
        GameManager.Instance.Resume();
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.GoToMainMenu();
    }
    public void Restart()
    {
        GameManager.Instance.ReloadScene();
       // GameManager.Instance.GoToScene("Tuto");
    }
}
