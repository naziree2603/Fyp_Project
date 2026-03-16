using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    private Rigidbody rb;
    private float lifeTime = 2f;
    [SerializeField] private int speed = 20;
    private Vector3 direction;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private int damage = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
        rb.linearVelocity = transform.forward * speed;
    }

    public void SetSpellDirection(Vector3 dr)
    {
        direction = dr.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
