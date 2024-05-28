public class MilestoneCompletionCondition : APlayerEventCondition
{
    Milestone[] milestones;


    public MilestoneCompletionCondition(params Milestone[] milestones) : base()
    {
        this.milestones = milestones;
    }

    public override bool IsFullfilled()
    {
        bool fullfilled = true;

        foreach (Milestone m in milestones)
        {
            if (!TextRPG.instance.player.IsGrantedMilestone(m))
            {
              fullfilled = false;
            }
        }

        return fullfilled;
    }
}