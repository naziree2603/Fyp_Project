using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject howToPlayMenu;
    public GameObject characterMenu;

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        characterMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        howToPlayMenu.SetActive(false);
        characterMenu.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
        characterMenu.SetActive(false);
    }

    public void OpenCharacterMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        characterMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

    public void StartCutScene()
    {
        SceneManager.LoadScene("CutScene");
    }

    public void NewGame()
    {
        Debug.Log("Starting NEW GAME");

        PlayerPrefs.SetInt("IsNewGame", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("CutScene");
    }
    public void ContinueGame()
    {
        PlayerPrefs.SetInt("IsNewGame", 0);
        SceneManager.LoadScene("GameScene");
    }
}