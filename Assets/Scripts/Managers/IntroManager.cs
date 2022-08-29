using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    //Inspector variables
    [SerializeField] private Transform playerTransform;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Player player;

    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject winOnTimePanel;
    [SerializeField] private GameObject fallOffEdgePanel;
    [SerializeField] private GameObject portalWinPanel;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip clip_30seconds;
    [SerializeField] private AudioClip clip_1minute;
    [SerializeField] private AudioClip clip_2minute;
    [SerializeField] private AudioClip clip_4minute;
    [SerializeField] private AudioClip clip_5minute;
    [SerializeField] private AudioClip clip_falloffstage;
    [SerializeField] private AudioClip clip_success;
    [SerializeField] private AudioClip clip_leavePortal;

    //Not in inspector
    [HideInInspector]
    public bool gamePaused;

    //Private variables
    private bool inMenus;
    private float startTime;
    private Vector3 startPosition;
    private bool hasMoved;
    private bool levelEndInProgress;
    private bool inPortal;
    private bool played30s;
    private bool played60s;
    private bool played2m;
    private bool played4m;
    private bool played5m;
    private float timeInPortal;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        startPosition = playerTransform.position;

        Time.timeScale = 1;
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

        //Player falls off the edge
        if(playerTransform.position.y <= -10 && !levelEndInProgress)
        {
            Debug.Log("Player fell off the stage.");
            levelEndInProgress = true;
            StartCoroutine(FallOffEdge());
        }

        //Player pauses game
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused && !inMenus)
            {
                //Pause game
                Debug.Log("Game Paused");
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                gamePaused = true;
                player.PauseMovement();
                if(audioSource.isPlaying)
                {
                    audioSource.Pause();
                }

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
                audioSource.UnPause();

                pausePanel.SetActive(false);
            }
        }

        //If the current position is different than the start position mark the player as moved
        if((startPosition.x != playerTransform.position.x || startPosition.z != playerTransform.position.z) && !hasMoved)
        {
            Debug.Log("Player has moved.");
            hasMoved = true;
        }

        //30 seconds of player standing still
        if(!hasMoved && currentTime > 30.0f && !audioSource.isPlaying && !played30s)
        {
            Debug.Log("30 seconds elapsed");
            
            audioSource.clip = clip_30seconds;
            audioSource.Play();
            played30s = true;
        }

        //1 minute has passed with player still
        if(!hasMoved && currentTime > 60.0f && !audioSource.isPlaying && !played60s)
        {
            Debug.Log("1 minute no move");

            audioSource.clip = clip_1minute;
            audioSource.Play();
            played60s = true;
        }

        //2 minutes with player still
        if(!hasMoved && currentTime > 120.0f && !audioSource.isPlaying && !played2m)
        {
            
            Debug.Log("2 minutes passed");

            audioSource.clip = clip_2minute;
            audioSource.Play();
            played2m = true;
        }

        //4 minutes with no movement
        if(!hasMoved && currentTime > 240.0f && !audioSource.isPlaying && !played4m)
        {
            
            Debug.Log("4 minutes passed");

            audioSource.clip = clip_4minute;
            audioSource.Play();
            played4m = true;
        } 

        //5 minutes pass and the player WINS on time
        if(!hasMoved && currentTime > 300.0f && !played5m)
        {
            
            Debug.Log("Player wins on time");

            played5m = true;
            WinOnTime();
        }
    }

    public IEnumerator PortalToNextLevel()
    {
        audioSource.clip = clip_success;
        audioSource.Play();

        yield return new WaitWhile(() => audioSource.isPlaying);

        portalWinPanel.SetActive(true);

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gamePaused = true;
        player.PauseMovement();
    }

    public IEnumerator WinOnTime()
    {
        Time.timeScale = 0;
        winOnTimePanel.SetActive(true);

        audioSource.clip = clip_5minute;
        audioSource.Play();

        yield return new WaitWhile(() => audioSource.isPlaying);

        winOnTimePanel.SetActive(true);
    }

    public IEnumerator FallOffEdge()
    {
        fallOffEdgePanel.SetActive(true);

        audioSource.clip = clip_falloffstage;
        audioSource.Play();

        yield return new WaitWhile(() => audioSource.isPlaying);

        fallOffEdgePanel.SetActive(true);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("BlueSquareLevel");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadOptions()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
        inMenus = true;
    }

    public void ReturnFromOptions()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
        inMenus = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayerEnterPortal()
    {
        Debug.Log("Portal timer started.");
        inPortal = true;
    }

    public void PlayerExitPortal()
    {
        Debug.Log("Portal timer stopped");
        inPortal = false;
        timeInPortal = 0f;

        audioSource.clip = clip_leavePortal;
        audioSource.Play();
    }
}
