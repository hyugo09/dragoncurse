using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameMode gameMode { get; private set; }

    public static GameManager Instance;

    public GameObject pauseMenuUI;

    public GameObject GameoverUI;

    

    // Start is called before the first frame update
    void Start()
    {
     
        gameMode = GameMode.PLAY;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
        {
            Destroy(this.gameObject);
        }

        

    }

    public void ChangeGameMode(GameMode newGameMode)
    {
        gameMode = newGameMode;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Additive);
    }

    public void GoToScene( string scene)
    {
        SceneManager.LoadScene(scene);
        
    }
    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        Instance.gameMode = GameMode.PAUSE;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

        Instance.gameMode = GameMode.PLAY;
    }
    public void ReloadScene()
    {
        GameoverUI.SetActive(false);
        string curentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(curentSceneName);
        Time.timeScale = 1f;
        Instance.gameMode = GameMode.PLAY;
    }
    public void Gameover()
    {
        GameoverUI.SetActive(true );
        Time.timeScale = 0f;

        Instance.gameMode = GameMode.PAUSE;
    }
}

public enum GameMode
{
    PLAY,
    PAUSE
}
