using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAi : MonoBehaviour
{
    private Transform playerTransform;
    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField] private float ChaseSight = 7f;
    [SerializeField] private float stopDistance = 2f;
    [SerializeField] private float AttackCoolDown = 2f;
    float timer = 2f;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playersLayer;
    [SerializeField] private int damage = 20;
    [SerializeField] private float WaitTimeMelee = 0.5f;

    [SerializeField] private bool isMage = false;

    [SerializeField] private GameObject Orb;

    private enemyProjectile enemySpell;
    private enemyHealth enemyHealth;
    private AudioSource audio;
    [SerializeField] private AudioClip[] attackSounds;
    [SerializeField] private AudioClip[] chaseSounds;
    [SerializeField] private AudioClip swordSounds;
    [SerializeField] private AudioClip magicSounds;
    private float soundTimer;
    [SerializeField] private float CoolDownSound = 3f;  





    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        enemyHealth = GetComponent<enemyHealth>();
        audio = GetComponent<AudioSource>();
        soundTimer = CoolDownSound; // Initialize sound timer to allow immediate sound play
    }

  
    void Update()
    {
        if (!enemyHealth.IsAlive) return;
        
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerTransform = player.transform;
            else
                return;
        }

        timer += Time.deltaTime;
        soundTimer += Time.deltaTime;
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if(distance <= stopDistance)
        {
            //attack
            agent.isStopped = true;
            RotateTowardsPlayer();
            if(timer >= AttackCoolDown)
            {
                anim.SetTrigger("Attack1");

                if (soundTimer >= CoolDownSound)
                    {
                        PlaySounds(attackSounds);
                        soundTimer = 0; // Reset sound timer after playing sound
                }

                if (!isMage)
                {
                    Invoke("MeleeAttack", WaitTimeMelee);
                }
                else
                {
                    Invoke("MagicAttack", WaitTimeMelee);
                }
                    timer = 0;
            }
            
            anim.SetBool("Run", false);
        }
        else if (distance <= ChaseSight && distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(playerTransform.position);
            anim.SetBool("Run", true);

            if (soundTimer >= CoolDownSound)
            {
                PlaySounds(chaseSounds);
                soundTimer = 0; // Reset sound timer after playing sound
            }
        }
        else
        {
            agent.ResetPath();
            anim.SetBool("Run", false);
        }




    }

    private void PlaySounds(AudioClip[] sounds)
    {
        int randomIndex = Random.Range(0, sounds.Length);
        audio.PlayOneShot(sounds[randomIndex]);
    }

    private void MeleeAttack()
    {
        audio.PlayOneShot(swordSounds);
        Collider[] players = Physics.OverlapSphere(attackPoint.position, attackRange, playersLayer);
        foreach (Collider target in players)
        {
            target.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private void MagicAttack()
    {
        audio.PlayOneShot(magicSounds);
        GameObject orbObject = Instantiate(Orb, attackPoint.position, Quaternion.identity);

        enemySpell = orbObject.GetComponent<enemyProjectile>();
        enemySpell.SetSpellDirection(transform.forward);

        enemySpell.transform.rotation = Quaternion.LookRotation(transform.forward);


    } 
    private void RotateTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0; // Keep the rotation on the horizontal plane
        if (direction != Vector3.zero)
        {
            Quaternion quaternion = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * agent.angularSpeed);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
