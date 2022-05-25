using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueSquareManager : MonoBehaviour
{
    //Visible to inspector
    [SerializeField] private int cubesToLoot = 0;
    [SerializeField] private Player player;

    [Header("Panels")]
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject winPanel;
    
    //Private
    private int cubesLooted = 0;
    private bool gamePaused = false;
    private bool inMenus = false;
    private bool panelLooted = false;

    private void Start() 
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gamePaused = false;
        player.UnpauseMovement();

        pausePanel.SetActive(false);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!gamePaused && !inMenus)
            {
                //Pause game
                Debug.Log("Game Paused");
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
                Debug.Log("Game unpaused");
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                gamePaused = false;
                player.UnpauseMovement();

                pausePanel.SetActive(false);
            }
        }
    }

    public void LootCube()
    {
        if(cubesToLoot == 0)
        {
            //Error checking
            Debug.Log("ERROR: Invalid number of cubes to loot.");
            return;
        }

        if(cubesLooted > cubesToLoot)
        {
            //Error checking
            Debug.Log("ERROR: All cubes looted end game.");
            return;
        }

        cubesLooted++;

        if(cubesLooted == cubesToLoot)
        {
            Debug.Log("All cubes have been looted");
        }
    }

    public void LootBlueButton()
    {
        Debug.Log("Blue button hit");
        
        if(panelLooted)
        {
            //Panel already found
        }
        else
        {
            LootCube();
            panelLooted = true; 
        }
    }

    public void FinishLevel()
    {
        inGamePanel.SetActive(false);
        winPanel.SetActive(true);

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gamePaused = true;
        player.PauseMovement();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("MirrorLevel");
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
        SceneManager.LoadScene("BlueSquareLevel");
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
