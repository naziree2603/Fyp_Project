using TMPro;
using UnityEditor;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
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

    public void Dialogue1()
    {
        Show();
        nameText.text = " ";
        dialogueText.text = "Teleport sound...";
    }

    public void Dialogue2()
    {
        Show();
        nameText.text = "King Birch";
        dialogueText.text = "So… the summoned Wira have arrived. How disappointing.";
    }

    public void Dialogue3()
    {
        Show();
        nameText.text = "King Birch";
        dialogueText.text = "My servants… Show them despair. Kill them.";
    }

    public void CutScene21()
    {
        Show();
        nameText.text = "Villager";
        dialogueText.text = "Help! Please, someone help me!";
    }

    public void CutScene22()
    {
        Show();
        nameText.text = "Enemy";
        dialogueText.text = "You cannot escape!";
    }

    public void CutScene23()
    {
        Show();
        nameText.text = "Villager";
        dialogueText.text = "Please! save me!";
    }

}