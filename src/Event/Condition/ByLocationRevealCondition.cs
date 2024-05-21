using System.Security;

public class ByLocationRevealCondition : APlayerEventCondition
{
    Location[] nodes;


    public ByLocationRevealCondition(Player player, params Location[] nodes) : base(player)
    {
        this.nodes = nodes;
    }

    public override bool IsFullfilled()
    {
        bool fullfilled = true;

        foreach (Location l in nodes)
        {
            if (!player.IsLocationRevealed(l))
            {
                fullfilled = false;
            }
        }

        return fullfilled;
    }
}