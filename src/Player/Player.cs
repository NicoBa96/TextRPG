public class Player
{
  string name = "Stepper";
  int health = 10;
  float stepFactor = 1;
  int walkedSteps = 0;

  Dictionary<Milestone, bool> milestoneMemory;

  public Player()
  {
    milestoneMemory = new Dictionary<Milestone, bool>();
    foreach (Milestone m in Milestone.ALL)
    {
      milestoneMemory.Add(m, false);
    }
  }

  public bool ActiveMilestone(Milestone m)
  {
    return milestoneMemory[m];
  }

  public void GrantMilestone(Milestone m)
  {
    milestoneMemory[m] = true;
    RPGWriter.Yellow("You recieved the Milestone: \"" + m.name + "\"!");
  }

  public int GetHealth()
  {
    return health;
  }

  public string GetName()
  {
    return name;
  }

  public int GetWalkedSteps()
  {
    return walkedSteps;
  }

  public int AddSteps(int stepAmount)
  {
    int newSteps = (int)(stepAmount * stepFactor);
    walkedSteps += newSteps;
    RPGWriter.Yellow("+ " + newSteps + " steps.");
    if (walkedSteps >= 20000)
    {
      GrantMilestone(Milestone.STEPS_20000);
    }
    return newSteps;
  }

}