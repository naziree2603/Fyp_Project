using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbarManager : MonoBehaviour
{
    public Slider slider;
    private enemyHealth currentEnemy;
    void Start()
    {
        slider.gameObject.SetActive(false);
    }

    public void UpdateEnemyHealthBar(enemyHealth enemy)
    {
        if (enemy != null)
        {
            currentEnemy = enemy;
            slider.maxValue = enemy.maxHealth;
            slider.value = enemy.currentHealth;
            slider.gameObject.SetActive(true);
        }
    }
    // no enemy

    public void HideHealthBar()
    {
        slider.gameObject.SetActive(false);
        currentEnemy = null;
    }


    void Update()
    {
        if(currentEnemy != null && slider.gameObject.activeSelf)
        {
            slider.value = currentEnemy.currentHealth;
        }
    }
}
