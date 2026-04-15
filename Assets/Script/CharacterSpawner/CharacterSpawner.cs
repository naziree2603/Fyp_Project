using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

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

        MiniMapFollow minimap = FindFirstObjectByType<MiniMapFollow>();
        if (minimap != null)
        {
            minimap.SetPlayer(player.transform);
        }
        MarkerManager.Instance.SetPlayer(player.transform);

        QuestMarker[] markers = FindObjectsByType<QuestMarker>(FindObjectsSortMode.None);

        foreach (var marker in markers)
        {
            marker.SetPlayer(player.transform);
        }


        StartCoroutine(DelayedInventorySetup(player));
    }
    IEnumerator DelayedInventorySetup(GameObject player)
    {
        yield return null; // wait 1 frame
        yield return new WaitForSeconds(0.2f); // small delay

        InventoryManager.instance.SetPlayer(player);

        InventoryManager.instance.InitializeAfterSpawn();
    }

    
}

