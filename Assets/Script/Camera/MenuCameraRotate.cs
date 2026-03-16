using UnityEngine;

public class MenuCameraRotate : MonoBehaviour
{
    public Transform mainMenuView;
    public Transform settingsView;
    public Transform characterSelectionView;
    public Transform howToPlayView;

    public float speed = 3f;

    private Transform target;

    void Start()
    {
        target = mainMenuView;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * speed);
    }

    public void GoToSettings()
    {
        target = settingsView;
    }

    public void BackToMenu()
    {
        target = mainMenuView;
    }

    public void GoToCharacterSelection()
    {
        target = characterSelectionView;
    }

    public void GoToHowToPlay()
    {
        target = howToPlayView;
    }
}