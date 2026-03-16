using System.Collections;
using UnityEngine;

public class AudioUnmute : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(UnmuteAfter3Seconds());
    }

    IEnumerator UnmuteAfter3Seconds()
    {
        yield return new WaitForSeconds(3f);

        audioSource.mute = false; // untick mute
    }
}