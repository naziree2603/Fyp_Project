using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

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

    
   


    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

  
    void Update()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerTransform = player.transform;
            else
                return;
        }

        timer += Time.deltaTime;
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if(distance <= stopDistance)
        {
            //attack
            agent.isStopped = true;
            RotateTowardsPlayer();
            if(timer >= AttackCoolDown)
            {
                anim.SetTrigger("Attack1");
                if(!isMage)
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
        }
        else
        {
            agent.ResetPath();
            anim.SetBool("Run", false);
        }


    }

    private void MeleeAttack()
    {
        Collider[] players = Physics.OverlapSphere(attackPoint.position, attackRange, playersLayer);
        foreach (Collider target in players)
        {
            target.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private void MagicAttack()
    {
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
