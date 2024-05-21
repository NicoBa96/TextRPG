public class MilestoneCompletionCondition : APlayerEventCondition
{
    Milestone[] milestones;


    public MilestoneCompletionCondition(Player player, params Milestone[] milestones) : base(player)
    {
        this.milestones = milestones;
    }

    public override bool IsFullfilled()
    {
        bool fullfilled = true;

        foreach (Milestone m in milestones)
        {
            if (!player.IsGrantedMilestone(m))
            {
              fullfilled = false;
            }
        }

        return fullfilled;
    }
}