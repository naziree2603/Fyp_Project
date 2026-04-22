using UnityEngine;

public class CutsceneAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            Debug.Log("Audio started");
        }
    }

    public void PauseSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
            Debug.Log("Audio paused");
        }
    }

    public void ResumeSound()
    {
        if (audioSource != null)
        {
            audioSource.UnPause();
            Debug.Log("Audio resumed");
        }
    }

    public void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            Debug.Log("Audio stopped");
        }
    }
}