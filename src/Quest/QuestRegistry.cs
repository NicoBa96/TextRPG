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
        .AddRecieveItemGoal("rig-01", Items.Letter, 1, true)
        .AddStepQuestReward(300)
        .AddStaminaQuestReward(3)
        .AddStartQuestText("You recieve a letter from the mayor, who wants you to deliver it to the coast.")
        .AddStartQuestText("His wife lives there, and he hasnt seen her in 3 months.")
        .AddEndQuestText("The mayors wife is happy to recieve the letter.")
        .AddEndQuestText("She allows you to take a walk in their backyard and rest in the guest house.");


        new Quest(this, QuestIdentifier.BringTheLetterBack)
        .SetName("Another Letter Delivery")
        .SetDescription("Deliver the letter to the city.")
        .AddReachLocationGoal("rloc-01", Locations.cityCentre)
        .AddRecieveItemGoal("rig-01", Items.Letter, 1, true)
        .AddStepFactorQuestReward(0.3f)
        .AddStartQuestText("The mayors wife wants you to return a letter she wrote as a reply")
        .AddStartQuestText("She promises you a great reward for this errand.")
        .AddEndQuestText("The mayor opens the letter before your eyes.")
        .AddEndQuestText("He starts crying and thanks you from all of his heart.")
        .AddEndQuestText("His secretary teaches you the old ancient royal way of stepping, improving your steps.");
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