using TMPro;
using UnityEngine;

public class CutScene3 : MonoBehaviour
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
        nameText.text = "Villager ";
        dialogueText.text = "You saved me… thank you. These creatures… they’ve taken over our land.";
    }

    public void Dialogue2()
    {
        Show();
        nameText.text = "Villager";
        dialogueText.text = "Ever since Birch became the Demon King… everything changed. You must find Dato' Maharaja Lela.";
    }

    public void Dialogue3()
    {
        Show();
        nameText.text = "Villager";
        dialogueText.text = "He is the only one who understands what is happening. He’s in the safe village… beyond this path.";
    }


}
