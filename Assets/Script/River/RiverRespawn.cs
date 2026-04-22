using UnityEngine;

public class RiverRespawn : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();

            // disable controller before teleport (IMPORTANT)
            if (cc != null) cc.enabled = false;

            other.transform.position = respawnPoint.position;

            // enable back
            if (cc != null) cc.enabled = true;

            Debug.Log("Player fell into river → respawn");
        }
    }
}