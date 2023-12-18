public class Player
{
  string playerName = "Stepper";
  int playerHealth = 10;
  int playerStepLength = 1;
  int walkedDistance = 0;

  public int GetPlayerHealth()
  {
    return playerHealth;
  }

  public string GetPlayerName()
  {
    return playerName;
  }

  public int GetWalkDistance()
  {
    return walkedDistance;
  }

}