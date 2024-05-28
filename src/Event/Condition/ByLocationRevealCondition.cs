using System.Security;

public class ByLocationRevealCondition : APlayerEventCondition
{
    Location[] nodes;


    public ByLocationRevealCondition(params Location[] nodes) : base()
    {
        this.nodes = nodes;
    }

    public override bool IsFullfilled()
    {
        bool fullfilled = true;

        foreach (Location l in nodes)
        {
            if (!TextRPG.instance.player.IsLocationRevealed(l))
            {
                fullfilled = false;
            }
        }

        return fullfilled;
    }
}