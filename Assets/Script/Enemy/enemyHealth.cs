using System.Collections;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private Animator anim;
    public bool IsAlive = true;

    private float hitCooldown = 0.5f;
    private float lastHit = -1;

    private EnemyHealthBarManager enemyHealthManager;
    private Coroutine hideHealthBarCoroutine;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
        IsAlive = true;

        // ✅ Better: use Instance instead of FindObjectOfType
        enemyHealthManager = EnemyHealthBarManager.Instance;

        if (enemyHealthManager == null)
        {
            Debug.LogError("EnemyHealthBarManager not found in scene!");
        }
    }

    public void TakeDamage(int damage)
    {
        if (!IsAlive) return;

        currentHealth = Mathf.Max(currentHealth - damage, 0);

        // ✅ Always update UI when hit
        if (enemyHealthManager != null)
        {
            enemyHealthManager.UpdateEnemyHealthBar(this);
        }

        // ✅ Restart hide coroutine properly
        if (hideHealthBarCoroutine != null)
        {
            StopCoroutine(hideHealthBarCoroutine);
        }
        hideHealthBarCoroutine = StartCoroutine(HideHealthBarDelay());

        // ✅ Play hit animation with cooldown
        if (Time.time > lastHit + hitCooldown)
        {
            anim.SetTrigger("Hit");
            lastHit = Time.time;
        }

        // ✅ Death check
        if (currentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        IsAlive = false;

        if (hideHealthBarCoroutine != null)
        {
            StopCoroutine(hideHealthBarCoroutine);
        }

        // ✅ Hide health bar on death
        if (enemyHealthManager != null)
        {
            enemyHealthManager.HideHealthBar();
        }

        anim.SetTrigger("Death");
        Invoke(nameof(DisableEnemy), 3f);
    }

    private IEnumerator HideHealthBarDelay()
    {
        yield return new WaitForSeconds(5f);

        if (enemyHealthManager != null)
        {
            enemyHealthManager.HideHealthBar();
        }
    }

    private void DisableEnemy()
    {
        Destroy(gameObject);
    }
}