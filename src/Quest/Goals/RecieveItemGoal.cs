public class RecieveItemGoal : AQuestGoal
{
   public AItem desiredItem;
   public bool consumeItemOnCompletion;
   public int desiredItemAmount;


    public RecieveItemGoal(Quest quest, string id, bool isExact, AItem desiredItem, int desiredItemAmount, bool consumeItemOnCompletion) : base(quest, id, isExact)
    {
        this.desiredItem = desiredItem;
        this.progressGoal = desiredItemAmount;
        this.desiredItemAmount = desiredItemAmount;
        this.consumeItemOnCompletion = consumeItemOnCompletion;
    }

    public override string GetDescription()
    {
        return $"Recieve the item {desiredItem.name} x {GetProgress()}/{progressGoal}";
    }

}