public record struct Milestone
{
    public static readonly List<Milestone> ALL = new List<Milestone>();
    public static readonly Milestone STEPS_20000 = new("20000 Steps", "Make 20000 steps on your journey");
    public static readonly Milestone WATCHCREDITS = new("Credit Watcher", "Open the credits once");
    public string name;

    public string description;

    public Milestone(string name, string description)
    {
        this.name = name;
        this.description = description;
        ALL.Add(this);
    }
}
