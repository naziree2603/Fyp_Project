using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutsceneUI : MonoBehaviour
{
    public GameObject mainMenuPanel;

    public void ShowMenu()
    {
        mainMenuPanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // change name if needed
    }
}