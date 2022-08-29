using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private GameObject optionsPanel;

    public void NewGame()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void LevelSelect()
    {
        //Switch canvas panels
        mainPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void LoadBlueSquare()
    {
        SceneManager.LoadScene("BlueSquareLevel");
    }

    public void LoadMirror()
    {
        SceneManager.LoadScene("MirrorLevel");
    }

    public void ReturnToMain()
    {
        //Switch canvas panels
        levelSelectPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OptionsPanel()
    {
        //Switch canvas panel
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void ReturnToMainFromOptions()
    {
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
