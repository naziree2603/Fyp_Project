using UnityEngine;
using Unity.Cinemachine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public CinemachineCamera cineCamera;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");

        GameObject player = Instantiate(
            characterPrefabs[selectedCharacter],
            transform.position,
            transform.rotation
        );

        // find the CameraFollow object inside the player
        Transform cameraTarget = player.transform.Find("CameraFollow");

        // assign camera follow
        cineCamera.Follow = cameraTarget;
        cineCamera.LookAt = cameraTarget;

        // assign camera to movement script
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        movement.SetCamera(Camera.main.transform);
    }
}