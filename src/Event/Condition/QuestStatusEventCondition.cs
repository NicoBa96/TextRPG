public class QuestStatusEventCondition : AEventCondition
{
    Quest quest;
    QuestStatus questStatus;

    public QuestStatusEventCondition(Quest quest, QuestStatus questStatus) : base()
    {
        this.quest = quest;
        this.questStatus = questStatus;
    }

    public override bool IsFullfilled()
    {
        return TextRPG.instance.player.questMemory.HasQuestSpecifiedStatus(quest, questStatus);
    }
}