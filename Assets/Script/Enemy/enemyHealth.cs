using UnityEngine;
using UnityEngine.Timeline;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    private Animator anim;
    public bool IsAlive = true;
    private float hitCooldown = 0.5f;
    private float lastHit = -1;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (!IsAlive) return;
        
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        if (currentHealth > 0 && Time.time > hitCooldown + lastHit)
        {
            anim.SetTrigger("Hit");
            lastHit = Time.time;
        }


            if (currentHealth <= 0)
        {
            EnemyDeath();
        }

    }

    private void EnemyDeath()
    {
        IsAlive = false;
        anim.SetTrigger("Death");
        Invoke("DisableEnemy", 3f); // Delay to allow death animation to play
    }

    private void DisableEnemy()
    {
        Destroy(gameObject);
    }


}
