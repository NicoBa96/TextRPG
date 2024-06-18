using System.Text.Json.Serialization;

public class QuestMemory
{
    [JsonInclude]
    public Dictionary<QuestIdentifier, QuestStatus> questStatus;

    [JsonInclude]
    public Dictionary<QuestIdentifier, Dictionary<string, float>> questProgress;

    public QuestMemory()
    {
        questStatus = new Dictionary<QuestIdentifier, QuestStatus>();
        questProgress = new Dictionary<QuestIdentifier, Dictionary<string, float>>();
    }

    public void StartQuest(QuestIdentifier questIdentifier)
    {
        if (questStatus.Any(q => q.Key == questIdentifier))
        {
            return;
        }

        Quest quest = TextRPG.instance.questRegistry.GetQuest(questIdentifier);
        questStatus.Add(questIdentifier, QuestStatus.InProgress);
        Dictionary<string, float> questGoalsProgress = new Dictionary<string, float>();
        for (int i = 0; i < quest.goals.Count; i++)
        {
            questGoalsProgress.Add(quest.goals[i].id, 0);
        }

        questProgress.Add(quest.id, questGoalsProgress);
        RPGWriter.Gain($"New Quest: {quest.name} - {quest.description}");
        foreach (string text in quest.startQuestText)
        {
            RPGWriter.Yellow(text);
        }
    }

    public List<Quest> GetAllQuestsByStatus(QuestStatus desiredStatus)
    {
        List<Quest> allActiveQuests = new List<Quest>();
        foreach (KeyValuePair<QuestIdentifier, QuestStatus> keyValuePair in questStatus)
        {
            if (keyValuePair.Value == desiredStatus)
            {
                try
                {
                    allActiveQuests.Add(TextRPG.instance.questRegistry.GetQuest(keyValuePair.Key));
                }
                catch (QuestNotFoundException)
                {
                    RPGWriter.Red($"Exception: Quest not found with id {keyValuePair.Key}");
                }

            }
        }

        return allActiveQuests;
    }

    public bool HasQuestSpecifiedStatus(QuestIdentifier questIdentifier, QuestStatus status)
    {
        if (!questStatus.ContainsKey(questIdentifier))
        {
          return status == QuestStatus.Open;  
        }

        return questStatus[questIdentifier] == status;
    }

    internal float GetQuestGoalProgress(AQuestGoal aQuestGoal)
    {
        Dictionary<string, float> progress = GetQuestProgress(aQuestGoal.quest);
        return progress[aQuestGoal.id];
    }

    internal void SetQuestGoalProgress(AQuestGoal aQuestGoal, float newProgress)
    {
        questProgress[aQuestGoal.quest.id][aQuestGoal.id] = newProgress;
    }

    private Dictionary<string, float> GetQuestProgress(Quest quest)
    {
        return questProgress.First(k => k.Key == quest.id).Value;
    }
}