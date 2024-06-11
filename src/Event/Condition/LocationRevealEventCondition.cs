using System.Security;

public class LocationRevealEventCondition : AEventCondition
{
    Location[] nodes;


    public LocationRevealEventCondition(params Location[] nodes) : base()
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