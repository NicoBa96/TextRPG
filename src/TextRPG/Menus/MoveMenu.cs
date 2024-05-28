public class MoveMenu
{
    public bool OpenMoveMenu()
    {
        CreateMoveMenu().HandleInput();
        return true;
    }

    public SelectionMenu CreateMoveMenu()
    {
        SelectionMenu menu = new SelectionMenu(() =>
        {
            RPGWriter.Blue("Location: " + TextRPG.instance.player.currentLocationName + ", you can travel to:");
            return true;
        });

        menu.AddEntry("0", "Back to Menu", () => false);

        List<Trail> paths = TextRPG.instance.map.GetPaths();
        for (int i = 0; i < paths.Count; i++)
        {
            Trail currentEdge = paths.ElementAt(i);
            Location targetNode = currentEdge.destinationNode == TextRPG.instance.map.GetCurrentLocation() ? currentEdge.startNode : currentEdge.destinationNode;
            if (TextRPG.instance.player.IsLocationRevealed(targetNode))
            {
                string entryname = String.Format("{0} [{1} steps]", targetNode.name, currentEdge.stepValue);
                menu.AddEntry("" + (i + 1), entryname, () => MovePlayerTo(currentEdge));
            }
            else
            {
                string entryname = (String.Format("{0} [{1} steps]", "???", "???"));
                menu.AddEntry("" + (i + 1), entryname, () => MovePlayerTo(currentEdge));
            }
        }

        menu.AddEntry("m", "Show Map", () =>
        {
            TextRPG.instance.player.GrantMilestone(Milestone.FIRST_MAP_USAGE);
            TextRPG.instance.map.DrawMap();
            return true;
        });

        return menu;
    }

    private bool MovePlayerTo(Trail trail)
    {
        Location newLocation = trail.destinationNode == TextRPG.instance.map.GetCurrentLocation() ? trail.startNode : trail.destinationNode;
        TextRPG.instance.map.SetCurrentLocation(newLocation);
        RPGWriter.Yellow("You have entered: " + newLocation.name + ", " + newLocation.description);
        RPGWriter.LineBreak();

        TextRPG.instance.player.RevealLocation(newLocation);
        TextRPG.instance.player.AddSteps(trail.stepValue);
        

        RPGWriter.LineBreak();
        TextRPG.instance.HandleEvent(newLocation);
        return false;
    }
}