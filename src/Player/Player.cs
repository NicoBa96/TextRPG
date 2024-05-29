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
  public QuestMemory questMemory;

  [JsonInclude]
  public Inventory inventory;

  [JsonInclude]
  public string currentLocationName;

  [JsonInclude]
  int chanceEventTriggerCount = 0;

  public Player()
  {
    milestoneMemory = new Dictionary<string, bool>();
    locationMemory = new Dictionary<string, bool>();
    questMemory = new QuestMemory();
    inventory = new Inventory();
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
    if (IsLocationRevealed(l))
    {
      return;
    }

    locationMemory[l.name] = true;
    RPGWriter.Gain("Location Knowledge: " + l.name);
    if (TextRPG.instance.map.nodes.All(IsLocationRevealed))
    {
      GrantMilestone(Milestone.EVERYTHING_REVEALED);
    }
  }

  public bool IsGrantedMilestone(Milestone m)
  {
    return milestoneMemory[m.name];
  }

  public void GrantMilestone(Milestone m)
  {
    if (milestoneMemory[m.name] == true) { return; }

    milestoneMemory[m.name] = true;
    RPGWriter.Gain("Milestone: " + m.name);
  }

  public int GetHealth()
  {
    return health;
  }

  public void Damage(int dmgAmount)
  {
    health -= dmgAmount;
    RPGWriter.Decrease($"{dmgAmount} health");
  }

  public void Heal(int healAmount)
  {
    health += healAmount;
    RPGWriter.Gain($"{healAmount} health");
  }

  public bool IsDead()
  {
    return health <= 0;
  }

  public string GetName()
  {
    return name;
  }

  public int GetWalkedSteps()
  {
    return walkedSteps;
  }

  public float GetStepFactor()
  {
    return stepFactor;
  }

  public int AddSteps(int stepAmount)
  {
    int newSteps = (int)(stepAmount * stepFactor);
    walkedSteps += newSteps;
    RPGWriter.Gain($"{newSteps} steps");
    if (walkedSteps >= 20000)
    {
      GrantMilestone(Milestone.STEPS_20000);
    }
    return newSteps;
  }

  public void ChangeStepFactor(float factorChange)
  {
    float oldStepFactor = stepFactor;
    stepFactor += factorChange;
    RPGWriter.Gain($"{factorChange} step factor ({oldStepFactor} => {stepFactor})");
  }

  public void AddChanceConditionTriggerCount()
  {
    chanceEventTriggerCount += 1;

    if (chanceEventTriggerCount == 5)
    {
      GrantMilestone(Milestone.FIFTH_CHANCE_CONDITION_EVENT);
    }
  }

  public void AddItemToInventory(AItem i)
  {
    inventory.AddItem(i);
  }
}