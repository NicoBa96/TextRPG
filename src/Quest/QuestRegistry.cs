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
         .AddReachLocationGoal("rloc-01", Locations.mountains)
         .AddStepFactorQuestReward(0.5f);

         new Quest(this, QuestIdentifier.DeliverLetterToCoast)
         .SetName("Letter Delivery")
         .SetDescription("Deliver the letter to the coast.")
         .AddReachLocationGoal("rloc-01", Locations.coast)
         .AddRecieveItemGoal("rig-01", Items.Letter, 1)
         .AddStepQuestReward(300)
         .AddStaminaQuestReward(3);
         
         

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