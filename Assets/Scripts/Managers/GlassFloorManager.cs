using UnityEngine;
using UnityEngine.SceneManagement;

public class GlassFloorManager : MonoBehaviour
{
    [SerializeField] private Player player;

    [Header("Panels")]
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject winPanel;

    private bool gamePaused;
    private bool inMenus;
    private float startTime;

    void Start()
    {
        //Reset cursor and player state at the start of each level
        Time.timeScale = 1;
        startTime = Time.time;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gamePaused = false;
        player.UnpauseMovement();

        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time - startTime;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!gamePaused && !inMenus)
            {
                //Pause game
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                gamePaused = true;
                player.PauseMovement();

                pausePanel.SetActive(true);
            }
            else if(gamePaused && !inMenus)
            {
                //Unpause game
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                gamePaused = false;
                player.UnpauseMovement();

                pausePanel.SetActive(false);
            }
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void LoadOptions()
    {
        pausePanel.SetActive(false);
        inGamePanel.SetActive(false);

        optionsPanel.SetActive(true);
        inMenus = true;
    }

    public void ReturnFromOptions()
    {
        optionsPanel.SetActive(false);

        pausePanel.SetActive(true);
        inGamePanel.SetActive(true);
        inMenus = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("GlassFloorMaze");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
