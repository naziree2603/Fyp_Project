using UnityEngine;

public class Quests
{
    public int ID;
    public string description;
    public enum questType { kill, trigger }
    public int requiredKills;
    public int currentKills;
    public questType type;
    public bool isComplete;

    public Quests (int id, string desc, int requiredK, questType typ)
    {
        ID = id;
        description = desc;
        requiredKills = requiredK;
        currentKills = 0;
        type = typ;
        isComplete = false;
    }

    public bool isCompleted => isComplete || CheckIfComplete();

    private bool CheckIfComplete()
    {
        if(type == questType.kill)
        {
            if(currentKills >= requiredKills)
            {
                isComplete = true;
            }
        }
        else if (type == questType.trigger)
          isComplete = true;

        return isComplete;
        
    }

    // method to increment kill count

    public void IncrementKillCount()
    {
        if(type == questType.kill)
        {
            currentKills++;
            CheckIfComplete();
        }
    }

    public void resetQuests()
    {
        currentKills = 0;
        isComplete = false;
    }


}
