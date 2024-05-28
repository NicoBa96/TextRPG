public abstract class AItem
{
    public string name;
    public string description;
    public int id;

    public AItem(int id, string name, string description)
    {
        this.id = id;
        this.name = name;
        this.description = description;
    }

    public override string ToString()
    {
        return $"{name} - {description}";
    }
}