public record struct Milestone
{
    public static readonly List<Milestone> ALL = new List<Milestone>();
    public static readonly Milestone STEPS_20000 = new("20000 Steps", "Make 20000 steps on your journey");
    public static readonly Milestone WATCH_CREDITS = new("Credit Watcher", "Open the credits once");
    public static readonly Milestone EVERYTHING_REVEALED = new("Everything revealed", "Reveal every location in the game");
    public static readonly Milestone FIRST_MAP_USAGE = new("Navigator", "Open the map once");
    public static readonly Milestone FIFTH_CHANCE_CONDITION_EVENT = new("Lucky Boy", "Trigger 5 chance-based events");
    public string name;

    public string description;

    public Milestone(string name, string description)
    {
        this.name = name;
        this.description = description;
        ALL.Add(this);
    }
}
