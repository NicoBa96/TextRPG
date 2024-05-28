public class NotRevealedCondition : APlayerEventCondition
{
    Location[] nodes;


    public NotRevealedCondition(params Location[] nodes) : base()
    {
        this.nodes = nodes;
    }

    public override bool IsFullfilled()
    {
        bool fullfilled = true;

        foreach (Location l in nodes)
        {
            if (TextRPG.instance.player.IsLocationRevealed(l))
            {
                fullfilled = false;
            }
        }

        return fullfilled;
    }
}