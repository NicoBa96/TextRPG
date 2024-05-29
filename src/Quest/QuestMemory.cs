using System.Text.Json.Serialization;

public class QuestMemory
{
    [JsonInclude]
    Dictionary<int, QuestStatus> questStatus;

    [JsonInclude]
    Dictionary<int, Dictionary<int, float>> questProgress;

    public QuestMemory()
    {
        questStatus = new Dictionary<int, QuestStatus>();
        questProgress = new Dictionary<int, Dictionary<int, float>>();
    }

    public void StartQuest(Quest quest)
    {
        questStatus.Add(quest.id, QuestStatus.InProgress);
        Dictionary<int, float> questGoalsProgress = new Dictionary<int, float>();
        for (int i = 0; i < quest.goals.Count; i++)
        {
            questGoalsProgress.Add(i, 0);
        }
        questProgress.Add(quest.id, questGoalsProgress);
    }
}