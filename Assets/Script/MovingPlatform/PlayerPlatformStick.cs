using UnityEngine;

public class PlayerPlatformStick : MonoBehaviour
{
    private CharacterController controller;

    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;

    public float rayLength = 1.5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        DetectPlatform();
    }

    void LateUpdate()
    {
        if (currentPlatform != null)
        {
            Vector3 platformDelta = currentPlatform.position - lastPlatformPosition;
            controller.Move(platformDelta);
            lastPlatformPosition = currentPlatform.position;
        }
    }

    void DetectPlatform()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.CompareTag("MovingPlatform"))
            {
                if (currentPlatform != hit.collider.transform)
                {
                    currentPlatform = hit.collider.transform;
                    lastPlatformPosition = currentPlatform.position;
                }
                return;
            }
        }

        currentPlatform = null;
    }
}