using Unity.Cinemachine;
using UnityEngine;

public class CutsceneEnd : MonoBehaviour
{
    public CinemachineCamera cutsceneCam;
    public CinemachineCamera playerCam;

    //public GameObject player;

    public void EndCutscene()
    {
        // Lower cutscene cam
        cutsceneCam.Priority = 5;

        // Raise player cam
        playerCam.Priority = 20;

        // Enable player control
        //player.SetActive(true);
    }


}