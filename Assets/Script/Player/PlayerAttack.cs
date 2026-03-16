using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private AudioSource audio;
    [SerializeField] private AudioClip meleeSound1;
    [SerializeField] private AudioClip meleeSound2;
    [SerializeField] private AudioClip meleeSound3;
    [SerializeField] private AudioClip meleeSound4;
    private float timer;
    [SerializeField] private float meleeCoolDown = 1f;

    private int attackCount = 0;
    private int attackAnimations = 4;

    private PlayerMovement playerMovement;
    bool isAttacking = false;

    [SerializeField] private Transform MeleePoint;
    [SerializeField] private float MeleeRange;
    [SerializeField] private LayerMask enemyLayers;

    [SerializeField] private float detectingEnemyDistance = 2f;

    //magic
    [SerializeField] private GameObject orb;
    [SerializeField] private Transform orbPlace;
    private float timer2;
    [SerializeField] private float MagicCoolDown = 1f;

    private SpellPlayer spellPlayer;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        audio = GetComponent<AudioSource>();
        timer = meleeCoolDown;

        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        FaceTowardsClosestEnemy();
    }

    private void OnSpell()
    {
        if (timer2 >= MagicCoolDown && !isAttacking)
        {
            anim.SetTrigger("Spell");
            Invoke ("PerformSpell", 0.2f);
            timer2 = 0;
        }
    }
    private void PerformSpell()
    {
        GameObject orbObject = Instantiate(orb, orbPlace.position, Quaternion.identity);
        spellPlayer = orbObject.GetComponent<SpellPlayer>();
        spellPlayer.SetSpellDirection(transform.forward);

        spellPlayer.transform.rotation = Quaternion.LookRotation(transform.forward);

    }

    private void OnMelee()
    {
        if (timer >= meleeCoolDown && !isAttacking)
        {
            meleeAttack();
            timer = 0f;
            playerMovement.Normalspeed = 1f;
            StartCoroutine(ReturnNormalSpeed());

        }

    }

    private void meleeAttack()
    {
       attackCount = (attackCount % attackAnimations) + 1;
        switch(attackCount)
        {
            case 1:
                anim.SetTrigger("Attack1");
                StartCoroutine(MeleeDamage(0.3f));
                audio.clip = meleeSound1;
                audio.Play(); 
                break;
            case 2:
                anim.SetTrigger("Attack2");
                StartCoroutine(MeleeDamage(0.3f));
                audio.clip = meleeSound2;
                audio.Play();
                break;
            case 3:
                anim.SetTrigger("Attack3");
                StartCoroutine(MeleeDamage(0.3f));
                audio.clip = meleeSound3;
                audio.Play();
                break;
            case 4:
                anim.SetTrigger("Attack4");
                StartCoroutine(MeleeDamage(0.3f));
                audio.clip = meleeSound4;
                audio.Play();
                break;
            default:
                break;
        }

    }

    private IEnumerator MeleeDamage(float delay)
    {
        yield return new WaitForSeconds(delay);
        Collider[] enemies = Physics.OverlapSphere(MeleePoint.position, MeleeRange, enemyLayers);
        foreach (Collider enemy in enemies)
        {
            enemy.GetComponent<enemyHealth>().TakeDamage(20);
        }
        
    }
    private IEnumerator ReturnNormalSpeed()
    {
        yield return new WaitForSeconds(meleeCoolDown);
        playerMovement.Normalspeed = playerMovement.Normalspeed2;
    }

    private void FaceTowardsClosestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(MeleePoint.position, detectingEnemyDistance, enemyLayers);
        if(enemies.Length > 0)
        {
            Collider ClosestEnemy = null;
            float nearestEnemyDistance = detectingEnemyDistance;
            foreach (Collider enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < nearestEnemyDistance)
                {
                    nearestEnemyDistance = distance;
                    ClosestEnemy = enemy;
                }
            }

            if(ClosestEnemy != null)
            {
                Vector3 directionToEnemy = (ClosestEnemy.transform.position - transform.position).normalized;
                directionToEnemy.y = 0; // Keep only the horizontal direction
                Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(MeleePoint.position, MeleeRange);
    }


}
