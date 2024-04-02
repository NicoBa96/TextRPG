public class Player
{
  string name = "Stepper";
  int health = 10;
  float stepFactor = 1;
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

  public int AddSteps(int stepAmount)
  {
    int newSteps = (int)(stepAmount*stepFactor);
    walkedSteps += newSteps;
    return newSteps;
  }

}