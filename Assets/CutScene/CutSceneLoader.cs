using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}