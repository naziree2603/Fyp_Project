using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroLogo : MonoBehaviour
{
    public CanvasGroup logo;

    void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        // Fade in
        while (logo.alpha < 1)
        {
            logo.alpha += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(3);

        // Fade out
        while (logo.alpha > 0)
        {
            logo.alpha -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("MainMenu");
    }
}