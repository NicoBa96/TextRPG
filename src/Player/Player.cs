public class Player
{
  string name = "Stepper";
  int health = 10;
  int stepFactor = 1;
  int walkedSteps = 0;


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

  public void AddSteps(int stepAmount)
  {
    walkedSteps += stepAmount;
  }

}