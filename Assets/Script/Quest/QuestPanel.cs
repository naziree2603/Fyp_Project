using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public int questID;
    public string questDescription;
    public int reqKills;

    public Text descText;
    public Image CheckedImage;

    public GameObject[] nextQuestPeople; // 🔥 MULTIPLE HOLDERS
    public GameObject nextQuestPanel;

    public Quests quest;
    [SerializeField] private Quests.questType questType;

    private void Awake()
    {
        if (descText == null)
            questDescription = descText.text;

        // disable next quest panel
        if (nextQuestPanel != null)
            nextQuestPanel.SetActive(false);

        // 🔥 FORCE disable ALL enemies at start (VERY IMPORTANT)
        DisableAllNextQuestEnemies();
    }

    private void Start()
    {
        RegisterQuestIfNeeded();
        CheckedImage.gameObject.SetActive(false);

        // 🔥 DOUBLE SAFETY (fix your bug)
        DisableAllNextQuestEnemies();
    }

    private void OnEnable()
    {
        RegisterQuestIfNeeded();

        if (quest != null)
            quest.isComplete = false;

        QuestManager.OnQuestProgressedChange += HandleQuestProgressChange;
        QuestManager.OnQuestCompleted += HandleQuestComplete;

        UpdatePanel();
    }

    private void OnDisable()
    {
        QuestManager.OnQuestProgressedChange -= HandleQuestProgressChange;
        QuestManager.OnQuestCompleted -= HandleQuestComplete;
    }

    // 🔥 CENTRAL FUNCTION (IMPORTANT)
    private void DisableAllNextQuestEnemies()
    {
        if (nextQuestPeople == null) return;

        foreach (GameObject obj in nextQuestPeople)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    private void RegisterQuestIfNeeded()
    {
        if (QuestManager.Instance == null) return;

        quest = QuestManager.Instance.GetQuest(questID);

        if (quest == null)
        {
            QuestManager.Instance.RegisterQuest(questID, questDescription, reqKills, questType);
            quest = QuestManager.Instance.GetQuest(questID);
        }
    }

    private void HandleQuestProgressChange(int updateQuestID)
    {
        if (updateQuestID == questID)
        {
            UpdatePanel();
        }
    }

    private void HandleQuestComplete(int completedQuestID)
    {
        if (completedQuestID == questID)
        {
            Debug.Log("Quest " + questID + " completed!");

            // show next quest panel
            if (nextQuestPanel != null)
                nextQuestPanel.SetActive(true);

            // 🔥 ACTIVATE ALL ENEMY HOLDERS
            if (nextQuestPeople != null)
            {
                foreach (GameObject obj in nextQuestPeople)
                {
                    if (obj != null)
                    {
                        obj.SetActive(true);
                        Debug.Log(obj.name + " activated");
                    }
                }
            }

            CheckedImage.gameObject.SetActive(true);
            UpdatePanel();
        }
    }

    public void UpdatePanel()
    {
        if (quest != null)
        {
            descText.text = quest.description;
        }
    }
}