using UnityEngine;

public class NPCWaypoint : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float waitTime = 2f;

    private int currentIndex = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (waypoints.Length > 0)
        {
            currentIndex = Random.Range(0, waypoints.Length);

            // OPTIONAL: teleport NPC to starting point
            transform.position = waypoints[currentIndex].position;
        }
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        if (isWaiting)
        {
            animator.SetFloat("Speed", 0f);

            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                currentIndex = (currentIndex + 1) % waypoints.Length;
            }
            return;
        }

        Transform target = waypoints[currentIndex];
        Vector3 direction = (target.position - transform.position);
        direction.y = 0; // ❗ IMPORTANT

        if (direction.magnitude > 0.2f)
        {
            Vector3 move = direction.normalized * speed * Time.deltaTime;
            move.y = 0; // ❗ prevent going up/down
            transform.position += move;
            transform.forward = direction;

            animator.SetFloat("Speed", 1f);
        }
        else
        {
            isWaiting = true;
        }
    }
}
