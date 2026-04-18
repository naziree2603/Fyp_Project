using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class enemyHealth : MonoBehaviour
{
    public int QuestID;
    public int maxHealth;
    public int currentHealth;
    [SerializeField] private int healAmount = 20;
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

    public VillagerState villager;

    public EnemyManager manager;


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

        PlayerHealth player = FindFirstObjectByType<PlayerHealth>();
        if (player != null)
        {
            player.Heal(healAmount);
        }


        QuestManager.Instance.UpdateQuestProgress(QuestID);

        if (hideHealthBarCoroutine != null)
        {
            StopCoroutine(HideHealthBarDelay());
        }

        PlaySounds(deathSounds);
        enemyHealthManager.HideHealthBar();
        anim.SetTrigger("Death");

        // drop loot
        List<Items> droppedLoot = InventoryManager.instance.GetRandomLoots();

        float xSpace = 1f;

        if(droppedLoot != null)
        {
            //foreach (var item in droppedLoot)
            //{
            //    Instantiate(item.groundedPrefab, transform.position, Quaternion.identity);
            //}

            for (int i = 0; i  < droppedLoot.Count; i++)
            {
                Vector3 spawnPos = transform.position + new Vector3(xSpace * i, 0, Random.Range(-2, 2));
                Instantiate(droppedLoot[i].groundedPrefab, spawnPos, Quaternion.identity);
            }

        }

        if (manager != null)
        {
            manager.EnemyDied();
        }

        if (villager != null)
        {
            villager.SetSafe();
        }

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
