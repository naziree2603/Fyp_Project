using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public int questID;
    public string questDescription;
    public int reqKills;
    public Text descText;
    public Image CheckedImage;
    public GameObject uncheckedImage;
    public GameObject nextQuestPeople;
    public GameObject nextQuestPanel;

    public Quests quest;
    [SerializeField] private Quests.questType questType;

    private void Awake()
    {
        if (descText == null)
            questDescription = descText.text;

        //ensure the next quest panel is not initially active

        if (nextQuestPanel != null && nextQuestPeople != null)
        {
            nextQuestPanel.SetActive(false);
            nextQuestPeople.SetActive(false);
        }
            
    }

    private void Start()
    {
        RegisterQuestIfNeeded();
        CheckedImage.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        RegisterQuestIfNeeded();

        if(quest == null)
        
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

    private void RegisterQuestIfNeeded()
    {
        if (QuestManager.Instance == null) return;

        quest = QuestManager.Instance.GetQuest(questID);

        if(quest == null)
        {
            //register the quest in the manager
            QuestManager.Instance.RegisterQuest(questID, questDescription, reqKills, questType);
            //create the quest instance after registering
            quest = new Quests(questID, questDescription, reqKills, questType);
        }
    }

    private void HandleQuestProgressChange(int updateQuestID)
    {
        if (updateQuestID == questID)
        {
            //update Panel
            UpdatePanel();
        }
    }

    private void HandleQuestComplete(int completedQuestID)
    {
        if(completedQuestID == questID)
        {
            if(nextQuestPanel != null) nextQuestPanel.SetActive(true);
            if(nextQuestPeople != null) nextQuestPeople.SetActive(true);
            CheckedImage.gameObject.SetActive(true);
            UpdatePanel();
        }
    }

    public void UpdatePanel()
    {
       if(quest != null)
        {
            descText.text = quest.description;
            
        }
    }
}
