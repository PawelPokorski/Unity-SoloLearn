using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Manager
{
    [SerializeField] protected List<Quest> quests = new ();

    void Update()
    {
        if(quests.Count == 0) return;

        foreach(var quest in quests)
        {
            quest.Update();

            if(quest.isCompleted)
            {
                quest.CompleteQuest(generalManager);
                quests.Remove(quest);
            }
        }
    }

    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
        quest.Initialize();
    }
}