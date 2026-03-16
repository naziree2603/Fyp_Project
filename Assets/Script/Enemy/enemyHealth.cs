using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public int MaxHealth;

    public void TakeDamage(int dmg)
    {
        MaxHealth -= dmg;
        Debug.Log("Enemy Health: " + MaxHealth);
    }
}
