public class RecieveItemGoal : AQuestGoal
{
   public AItem desiredItem;


    public RecieveItemGoal(Quest quest, string id, bool isExact, AItem desiredItem, int desiredItemAmount) : base(quest, id, isExact)
    {
        this.desiredItem = desiredItem;
        this.progressGoal = desiredItemAmount;
    }

    public override string GetDescription()
    {
        return $"Recieve the item {desiredItem.name} x {GetProgress()}/{progressGoal}";
    }

}