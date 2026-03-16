using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characters;
    private int currentCharacter = 0;

    void Start()
    {
        ShowCharacter();
    }

    public void NextCharacter()
    {
        currentCharacter++;
        if (currentCharacter >= characters.Length)
            currentCharacter = 0;

        ShowCharacter();
    }

    public void PreviousCharacter()
    {
        currentCharacter--;
        if (currentCharacter < 0)
            currentCharacter = characters.Length - 1;

        ShowCharacter();
    }

    void ShowCharacter()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(i == currentCharacter);
        }
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacter);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
