public class QuestNotFoundException : Exception
{
    public QuestNotFoundException(QuestIdentifier id) : base($"The Quest with the id {id} does not exist.")
    {

    }
}