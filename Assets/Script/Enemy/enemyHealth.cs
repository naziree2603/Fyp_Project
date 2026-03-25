using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class enemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    private Animator anim;
    public bool IsAlive = true;
    private float hitCooldown = 0.5f;
    private float lastHit = -1;
    private EnemyHealthbarManager enemyHealthManager;
    private Coroutine hideHealthBarCoroutine;

    private AudioSource audio;
    [SerializeField] private AudioClip[] deathSounds;
    
    private float soundTimer;
    [SerializeField] private float CoolDownSound = 3f;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
        IsAlive = true;
        enemyHealthManager = FindFirstObjectByType<EnemyHealthbarManager>();
        audio = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (!IsAlive) return;

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        if (currentHealth > 0 && Time.time > hitCooldown + lastHit)
        {
            enemyHealthManager.UpdateEnemyHealthBar(this);
            if (hideHealthBarCoroutine != null)
            {
                StopCoroutine(HideHealthBarDelay());
            }
            
            hideHealthBarCoroutine = StartCoroutine(HideHealthBarDelay());
            anim.SetTrigger("Hit");
            lastHit = Time.time;
        }


        if (currentHealth <= 0)
        {
            EnemyDeath();
        }

    }

    private void PlaySounds(AudioClip[] sounds)
    {
        int randomIndex = Random.Range(0, sounds.Length);
        audio.PlayOneShot(sounds[randomIndex]);
    }

    private void EnemyDeath()
    {
        IsAlive = false;
        
        if (hideHealthBarCoroutine != null)
        {
            StopCoroutine(HideHealthBarDelay());
        }

        PlaySounds(deathSounds);
        enemyHealthManager.HideHealthBar();
        anim.SetTrigger("Death");
        Invoke("DisableEnemy", 5f); // Delay to allow death animation to play
    }

    private IEnumerator HideHealthBarDelay()
    {
        yield return new WaitForSeconds(5); // Adjust the delay as needed
        enemyHealthManager.HideHealthBar();
    }

    private void DisableEnemy()
    {
        Destroy(gameObject);
    }


}
