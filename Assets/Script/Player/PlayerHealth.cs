using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private int maxHealth = 100;
    public int CurrentHealth;
    public bool isAlive = true;
    void Start()
    {
        HealthSlider.maxValue = maxHealth;
        CurrentHealth = maxHealth;
        HealthSlider.value = CurrentHealth;
    }

    public void TakeDamage(int dmg)
    {
        if (!isAlive) return;
        CurrentHealth = Mathf.Max(CurrentHealth - dmg, 0);
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
        //disable player movement
    }

    // Update is called once per frame
    void Update()
    {
   
           


    }
}
