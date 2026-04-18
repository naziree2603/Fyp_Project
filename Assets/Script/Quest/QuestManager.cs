using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    private Dictionary<int, Quests> quests = new Dictionary<int, Quests>();
    public static event Action<int> OnQuestProgressedChange;
    public static event Action<int> OnQuestCompleted;
    [SerializeField] private GameObject questPanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

    }
    void Start()
    {
        questPanel.transform.localPosition = new Vector3(2000, 2000, 0);
        //loop through each quest in the dictionary and set isComplete to false

        foreach (var quest in quests.Values)
        {
            quest.resetQuests();
        }
    }

    public void RegisterQuest (int questID, string desc, int reqKill, Quests.questType type)
    {
        if (!quests.ContainsKey(questID))
        {
            var quest = new Quests(questID, desc, reqKill, type);
            quests.Add(questID, quest);
        }
    }

    public void CompleteTriggerQuest(int questID)
    {
        if(quests.TryGetValue(questID, out Quests quest))
        {
            if(quest.type == Quests.questType.trigger)
            {
                quest.isComplete = true; // 🔥 THIS IS MISSING

                OnQuestProgressedChange?.Invoke(questID);
                OnQuestCompleted?.Invoke(questID);
            }
        }
    }

    public Quests GetQuest(int questID)
    {
        quests.TryGetValue(questID, out Quests quest);
        return quest;
    }

    public void UpdateQuestProgress(int questID)
    {
        if(quests.TryGetValue(questID, out Quests quest))
        {
            if(quest.type == Quests.questType.kill)
            {
                quest.IncrementKillCount();
                OnQuestProgressedChange?.Invoke(questID);
                if(quest.isCompleted)
                    OnQuestCompleted?.Invoke(questID);
            }
        }
    }


    // to close and open quest panel without deactivating it to keep script alive
    public void OpenPanel()
    {

        questPanel.transform.localPosition = new Vector3(0, 0, 0);

        //questPanel.SetActive(true);

        // force refresh of all questPanel children

        foreach (var qp in questPanel.GetComponentsInChildren<QuestPanel>(true))
        {
            qp.UpdatePanel();
        }
    }

    public void ClosePanel()
    {
        questPanel.transform.localPosition = new Vector3(2000, 2000, 0);

        //questPanel.SetActive(false);
    }

    void ToggleQuest()
    {
        // check if open (position 0 = open)
        if (questPanel.transform.localPosition.x == 0)
        {
            ClosePanel();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            OpenPanel();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleQuest();
        }
    }
}
