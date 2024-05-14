using System.Text.Json.Serialization;

public class Player
{
  [JsonInclude]
  string name = "Stepper";
  [JsonInclude]
  int health = 10;
  [JsonInclude]
  float stepFactor = 1;
  [JsonInclude]
  int walkedSteps = 0;

  [JsonInclude]
  Dictionary<string, bool> milestoneMemory;

  [JsonInclude]
  Dictionary<string, bool> locationMemory;

  [JsonInclude]
  public string currentLocationName;

  public Player()
  {
    milestoneMemory = new Dictionary<string, bool>();
    locationMemory = new Dictionary<string, bool>();
    foreach (Milestone m in Milestone.ALL)
    {
      milestoneMemory.Add(m.name, false);
    }
  }

  public bool IsLocationRevealed(Location l)
  {
    if (locationMemory.TryGetValue(l.name, out bool isRevealed))
    {
      return isRevealed;
    }
    return false;
  }

  public void RevealLocation(Location l)
  {
    locationMemory[l.name] = true;
  }

  public bool IsGrantedMilestone(Milestone m)
  {
    return milestoneMemory[m.name];
  }

  public void GrantMilestone(Milestone m)
  {
    if (milestoneMemory[m.name] == true) { return; }

    milestoneMemory[m.name] = true;
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