using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UI;

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

        // 🎥 Camera setup
        Transform cameraTarget = player.transform.Find("CameraFollow");

        cineCamera.Follow = cameraTarget;
        cineCamera.LookAt = cameraTarget;

        // 🎮 Movement setup
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        movement.SetCamera(Camera.main.transform);

        // 🔥 NEW: Connect Slider to EnemyHealthBarManager
        Slider slider = player.GetComponentInChildren<Slider>();

        EnemyHealthBarManager manager = FindObjectOfType<EnemyHealthBarManager>();

        if (manager != null && slider != null)
        {
            manager.SetSlider(slider);
        }
        else
        {
            Debug.LogWarning("Slider or EnemyHealthBarManager not found!");
        }
    }
}