using System.Reflection;

public static class Locations
{
    private static List<Location> _all;
    public static List<Location> ALL
    {
        get
        {
            if (_all != null)
            {
                return _all;
            }

            _all = typeof(Locations).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Select(f => f.GetValue(null))
            .OfType<Location>()
            .ToList();
            return _all;
        }
    }

    public static Location cityCentre = new Location(12, 12, ConsoleColor.Blue, "City Centre", "The main square of a large city");
    public static Location cityOutskirts = new Location(12, 14, ConsoleColor.Cyan, "City Outskirts", "The quiet and peacefull suburbs of the city");
    public static Location forest = new Location(14, 16, ConsoleColor.Green, "Forest", "A dark forest filled with trees");
    public static Location mountains = new Location(11, 18, ConsoleColor.Gray, "Mountains", "Large mountain peaks with a cold climate surrounding them");
    public static Location mountainTop = new Location(12, 18, ConsoleColor.DarkGray, "Mountaintop", "Standing on the peak of the mountain range, you can see as far as never before");
    public static Location coast = new Location(9, 20, ConsoleColor.Yellow, "Coast", "Vast coastline seperating the land from the endless sea");
    public static Location desert = new Location(7, 18, ConsoleColor.DarkYellow, "Desert", "The heat of this sandy desert is a challange for many");


}