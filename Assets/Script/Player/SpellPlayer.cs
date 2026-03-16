using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private float lifeTime = 2f;
    [SerializeField] private int speed = 20;
    private Vector3 direction;
    [SerializeField] private GameObject hitEffect;
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
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
