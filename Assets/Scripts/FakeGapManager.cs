using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FakeGapManager : MonoBehaviour
{
    [SerializeField] private Player player;

    [Header("Panels")]
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject winPanel;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip_leavePortal;
    [SerializeField] private AudioClip clip_success;

    private bool gamePaused;
    private bool inMenus;
    private float startTime;
    private bool inPortal;
    private float timeInPortal;
    private bool levelEndInProgress;

    // Start is called before the first frame update
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

        if(inPortal)
        {
            timeInPortal += Time.deltaTime;
            
            //Player has spent more than 5 seconds within portal bounds
            if(timeInPortal >= 5.0f && !levelEndInProgress)
            {
                Debug.Log("Player spent 5 seconds within portal.");
                levelEndInProgress = true;
                StartCoroutine(PortalToNextLevel());
            }
        }

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

    public void PlayerEnterPortal()
    {

    }

    public void PlayerExitPortal()
    {
        Debug.Log("Portal timer stopped");
        inPortal = false;
        timeInPortal = 0f;

        audioSource.clip = clip_leavePortal;
        audioSource.Play();
    }

    public IEnumerator PortalToNextLevel()
    {
        audioSource.clip = clip_success;
        audioSource.Play();

        yield return new WaitWhile(() => audioSource.isPlaying);

        FinishLevel();

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gamePaused = true;
        player.PauseMovement();
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
        SceneManager.LoadScene("MainMenu");
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
        SceneManager.LoadScene("FakeGapLevel");
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
