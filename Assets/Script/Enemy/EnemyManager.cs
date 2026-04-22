using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies; // drag enemies here
    public GameObject cutsceneTrigger;

    public void CheckAllDead()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null) // still alive
                return;
        }

        Debug.Log("All enemies dead!");

        if (cutsceneTrigger != null)
            cutsceneTrigger.SetActive(true);
    }
}