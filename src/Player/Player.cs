using System.Diagnostics;
using System.Text.Json.Serialization;

public class Player
{
  [JsonInclude]
  string name = "Stepper";
  [JsonInclude]
  int stamina = 10;
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
    UpdateReachLocationGoals(l);

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

  #region GoalUpdaters
  private void UpdateReachLocationGoals(Location l)
  {
    List<Quest> quests = questMemory.GetAllQuestsByStatus(QuestStatus.InProgress);
    foreach (Quest q in quests)
    {
      foreach (ReachLocationGoal reachLocationGoal in q.goals.OfType<ReachLocationGoal>())
      {
        if (reachLocationGoal.goalLocation == l)
        {
          reachLocationGoal.ChangeProgress(1);
        }
      }
    }
  }

  public void UpdateRecieveItemGoal(AItem item, int itemAmount)
  {
    List<Quest> quests = questMemory.GetAllQuestsByStatus(QuestStatus.InProgress);
    foreach (Quest q in quests)
    {
      foreach (RecieveItemGoal recieveItemGoal in q.goals.OfType<RecieveItemGoal>())
      {
        if (recieveItemGoal.desiredItem == item)
        {
          recieveItemGoal.ChangeProgress(itemAmount);
        }
      }
    }
  }

  private void UpdateStepCountGoal(int stepAmount)
  {
    List<Quest> quests = questMemory.GetAllQuestsByStatus(QuestStatus.InProgress);
    foreach (Quest q in quests)
    {
      foreach (StepCountGoal stepCountGoal in q.goals.OfType<StepCountGoal>())
      {
        stepCountGoal.ChangeProgress(stepAmount);
      }
    }
  }

  private void UpdateStaminaAmountGoal(int staminaAmount)
  {
    List<Quest> quests = questMemory.GetAllQuestsByStatus(QuestStatus.InProgress);
    foreach (Quest q in quests)
    {
      foreach (StaminaAmountGoal staminaAmountGoal in q.goals.OfType<StaminaAmountGoal>())
      {
        staminaAmountGoal.ChangeProgress(staminaAmount);
      }
    }
  }

  private void UpdateStepFactorValueGoal(float stepFactorValue)
  {
    List<Quest> quests = questMemory.GetAllQuestsByStatus(QuestStatus.InProgress);
    foreach (Quest q in quests)
    {
      foreach (StepFactorValueGoal stepFactorValueGoal in q.goals.OfType<StepFactorValueGoal>())
      {
        stepFactorValueGoal.ChangeProgress(stepFactorValue);
      }
    }
  }
  #endregion

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

  public int GetStamina()
  {
    return stamina;
  }

  public void Exhaust(int exhAmount)
  {
    stamina -= exhAmount;
    RPGWriter.Decrease($"{exhAmount} stamina");
    UpdateStaminaAmountGoal(exhAmount);
  }

  public void Replenish(int repAmount)
  {
    stamina += repAmount;
    RPGWriter.Gain($"{repAmount} stamina");
    UpdateStaminaAmountGoal(repAmount);
  }

  public bool IsDead()
  {
    return stamina <= 0;
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

    UpdateStepCountGoal(stepAmount);
    return newSteps;
  }

  public void ChangeStepFactor(float factorChange)
  {
    float oldStepFactor = stepFactor;
    stepFactor += factorChange;
    if (factorChange >= 0)
    {
      RPGWriter.Gain($"{factorChange} step factor ({oldStepFactor} => {stepFactor})");
    }
    else
    {
      RPGWriter.Decrease($"{factorChange} step factor ({oldStepFactor} => {stepFactor})");
    }
    UpdateStepFactorValueGoal(factorChange);
  }

  public void AddChanceConditionTriggerCount()
  {
    chanceEventTriggerCount += 1;

    if (chanceEventTriggerCount == 5)
    {
      GrantMilestone(Milestone.FIFTH_CHANCE_CONDITION_EVENT);
    }
  }

  public void AddItemToInventory(AItem i, int amount = 1)
  {
    inventory.AddItem(i, amount);
  }
}