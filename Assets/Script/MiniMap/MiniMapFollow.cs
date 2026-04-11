using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 20, 0); // Offset from the player

    public void SetPlayer(Transform target)
    {
        player = target;
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
