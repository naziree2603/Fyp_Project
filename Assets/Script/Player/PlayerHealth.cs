using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class PlayerHealth : MonoBehaviour
{
    
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private int maxHealth = 100;
    private CharacterController controller;
    public int CurrentHealth;
    public bool isAlive = true;

    public Transform respawnPoint;

    public int ShieldValue;

    private Animator anim;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();

        if (respawnPoint == null)
        {
            GameObject obj = GameObject.FindWithTag("Respawn");
            if (obj != null)
            {
                respawnPoint = obj.transform;
            }
            else
            {
                Debug.LogWarning("No Respawn point found!");
            }
        }
        HealthSlider.maxValue = maxHealth;
        CurrentHealth = maxHealth;
        HealthSlider.value = CurrentHealth;
        UpdateDefenceValue();
    }

    public void UpdateDefenceValue()
    {
        ShieldValue = InventoryManager.instance.GetShieldValue();  
        //Debug.Log("Shield Value: " + ShieldValue);
    }

    public void TakeDamage(int dmg)
    {
        if (!isAlive) return;

        int totalDefence = ShieldValue;
        int finalDamage = Mathf.Max(dmg - totalDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - finalDamage, 0);
        HealthSlider.value = CurrentHealth;
        if (CurrentHealth <= 0)
        {
            Death();
        }
        
    }



    private void Death()
    {
        isAlive = false;
        CurrentHealth = 0;
        HealthSlider.value = CurrentHealth;
        Debug.Log("Player Died");
        //play death animation
        anim.SetTrigger("Death");
        //disable player movement
        GetComponent<PlayerMovement>().enabled = false;

        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(5f); // match death animation

        // disable controller before teleport (IMPORTANT)
        if (controller != null) controller.enabled = false;

        // teleport to respawn point
        transform.position = respawnPoint.position;

        // enable back
        if (controller != null) controller.enabled = true;

        // reset health
        CurrentHealth = maxHealth;
        HealthSlider.value = CurrentHealth;

        // reset state
        isAlive = true;

        // 🎮 enable movement again
        GetComponent<PlayerMovement>().enabled = true;

        Debug.Log("Player Respawned");
    }

    public void Heal(int amount)
    {
        if (!isAlive) return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
        HealthSlider.value = CurrentHealth;

        Debug.Log("Player healed: +" + amount);
    }

    // Update is called once per frame
    void Update()
    {
   
           


    }
}
