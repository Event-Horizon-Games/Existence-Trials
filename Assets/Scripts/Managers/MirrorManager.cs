using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorManager : MonoBehaviour
{
    //Visible to inspector
    [SerializeField] private Player player;

    [Header("Panels")]
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject winPanel;

    //Private to this class
    private bool inMirrorCollider;
    private bool gamePaused = false;
    private bool inMenus = false;
    private float startTime;

    private void Start() 
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

        float currentTime = Time.time;

        if(inMirrorCollider && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Pressed Q in collider.");
        }
    }
    public void FinishLevel()
    {
        //inGamePanel.SetActive(false);
        winPanel.SetActive(true);

        Time.timeScale = 0; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gamePaused = true;
        player.PauseMovement();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("FakeGapLevel");
    }

    public void LoadOptions()
    {
        pausePanel.SetActive(false);
        //inGamePanel.SetActive(false);

        optionsPanel.SetActive(true);
        inMenus = true;
    }

    public void ReturnFromOptions()
    {
        optionsPanel.SetActive(false);

        pausePanel.SetActive(true);
        //inGamePanel.SetActive(true);

        inMenus = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("MirrorLevel");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayerEnterMirrorCollider()
    {
        Debug.Log("Player in range of mirror");
        inMirrorCollider = true;
    }

    public void PlayerLeaveMirrorCollider()
    {
        Debug.Log("Player left mirror range");
        inMirrorCollider = false;
    }
}
