using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 2f;

    private Transform target;
    private bool isWaiting = false;

    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        if (!isWaiting)
        {
            MovePlatform();
        }
    }

    void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            StartCoroutine(WaitAndSwitch());
        }
    }

    IEnumerator WaitAndSwitch()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        target = (target == pointA) ? pointB : pointA;
        isWaiting = false;
    }
}