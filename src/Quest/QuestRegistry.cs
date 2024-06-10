using System.Security.Cryptography.X509Certificates;

public class QuestRegistry
{
    public List<Quest> allQuests = new List<Quest>();

    public QuestRegistry(TextRPG textRPG)
    {
        CreateQuest(textRPG);
    }

    private void CreateQuest(TextRPG textRPG)
    {
        new Quest(this, QuestIdentifier.StartQuest)
         .SetName("Mudda Meeting")
         .SetDescription("Go meet your mudda at the mountains.")
         .AddReachLocationGoal("rloc-01", Locations.mountains);
    }

    public Quest GetQuest(QuestIdentifier id)
    {
        foreach (Quest q in allQuests)
        {
            if (q.id == id)
            {
                return q;
            }
        }

        throw new QuestNotFoundException(id);
    }

    internal void AddQuest(Quest quest)
    {
        if (allQuests.Any(q => q.id == quest.id))
        {
            return;
        }

        allQuests.Add(quest);
    }
}