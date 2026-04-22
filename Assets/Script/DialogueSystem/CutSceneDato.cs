using TMPro;
using UnityEngine;

public class CutSceneDato : MonoBehaviour
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
        nameText.text = "Dato' Maharaja Lela";
        dialogueText.text = "So… you are the Wira spoken of… the one who can defeat King Birch...";
    }

    public void DialogueD2()
    {
        Show();
        nameText.text = "Dato' Maharaja Lela";
        dialogueText.text = "Pasir Salak is no longer safe. The village has fallen under the control of Birch’s forces...";
    }

    public void DialogueD3()
    {
        Show();
        nameText.text = "Dato' Maharaja Lela";
        dialogueText.text = "His soldiers roam freely… terrorizing the people and claiming what is not theirs.";
    }

    public void DialogueD4()
    {
        Show();
        nameText.text = "Dato' Maharaja Lela";
        dialogueText.text = "Clear the enemies across this village...Leave none standing...";
    }
}
