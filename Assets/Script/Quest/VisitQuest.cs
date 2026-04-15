using UnityEngine;

public class VisitQuest : MonoBehaviour
{
    public int QuestID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            QuestManager.Instance.CompleteTriggerQuest(QuestID);
            Destroy(gameObject);

            // start a cutscene or activate new enemy gameobject
        }
    }
}


