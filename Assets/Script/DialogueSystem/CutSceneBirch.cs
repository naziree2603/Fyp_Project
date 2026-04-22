using TMPro;
using UnityEngine;

public class CutSceneBirch : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject panel;



    public void Show()
    {
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    public void DialogueD1()
    {
        Show();
        nameText.text = "King Birch";
        dialogueText.text = "So… you’re the Wira...";
    }

    public void DialogueD2()
    {
        Show();
        nameText.text = "King Birch";
        dialogueText.text = "You’ve made quite a mess of my forces.";
    }

    public void DialogueD3()
    {
        Show();
        nameText.text = "King Birch";
        dialogueText.text = "But this is where it ends. Come… and fall like the rest.";
    }

    public void DialogueD4()
    {
        Show();
        nameText.text = "King Birch";
        dialogueText.text = "Clear the enemies across this village...Leave none standing...";
    }
}
