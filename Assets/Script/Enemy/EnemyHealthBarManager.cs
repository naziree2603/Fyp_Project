using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarManager : MonoBehaviour
{
    public Slider slider;
    private enemyHealth currentEnemy;

    // ✅ Optional (better performance than FindObjectOfType)
    public static EnemyHealthBarManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetSlider(Slider newSlider)
    {
        slider = newSlider;

        if (slider != null)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Slider not assigned!");
        }
    }

    public void UpdateEnemyHealthBar(enemyHealth enemy)
    {
        if (enemy == null || slider == null) return;

        currentEnemy = enemy;

        slider.maxValue = enemy.maxHealth;
        slider.value = enemy.currentHealth;

        if (!slider.gameObject.activeSelf)
        {
            slider.gameObject.SetActive(true);
        }
    }

    public void HideHealthBar()
    {
        if (slider == null) return;

        slider.gameObject.SetActive(false);
        currentEnemy = null;
    }

    void Update()
    {
        if (slider == null) return;

        // ✅ If enemy destroyed → auto hide
        if (currentEnemy == null)
        {
            if (slider.gameObject.activeSelf)
            {
                slider.gameObject.SetActive(false);
            }
            return;
        }

        // ✅ Update health smoothly
        if (slider.gameObject.activeSelf)
        {
            slider.value = currentEnemy.currentHealth;
        }
    }
}