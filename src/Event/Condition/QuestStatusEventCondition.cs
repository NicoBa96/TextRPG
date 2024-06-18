public class QuestStatusEventCondition : AEventCondition
{
    QuestIdentifier questIdentifier;
    QuestStatus questStatus;

    public QuestStatusEventCondition(QuestIdentifier questIdentifier, QuestStatus questStatus) : base()
    {
        this.questIdentifier = questIdentifier;
        this.questStatus = questStatus;
    }

    public override bool IsFullfilled()
    {
        return TextRPG.instance.player.questMemory.HasQuestSpecifiedStatus(questIdentifier, questStatus);
    }
}