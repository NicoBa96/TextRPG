public class StartQuestReward : IReward
{
    QuestIdentifier questIdentifier;
    public StartQuestReward(QuestIdentifier questIdentifier)
    {
        this.questIdentifier = questIdentifier;
    }

    public void Grant()
    {
        TextRPG.instance.player.questMemory.StartQuest(questIdentifier);
    }
}