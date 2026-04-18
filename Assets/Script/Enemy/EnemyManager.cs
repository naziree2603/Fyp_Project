using UnityEngine;
using UnityEngine.Playables;

public class EnemyManager : MonoBehaviour
{
    public int enemyCount;
    public VillagerState villager;
    public PlayableDirector director;

    public void EnemyDied()
    {
        enemyCount--;

        if (enemyCount <= 0)
        {
            villager.SetSafe();   // villager relax
            director.Play();      // play timeline (optional)
        }
    }
}