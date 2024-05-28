using System.Net;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

public static class Items
{
    private static List<AItem> _all;
    public static List<AItem> ALL
    {
        get
        {
            if (_all != null)
            {
                return _all;
            }

            _all = typeof(Items).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Select(f => f.GetValue(null))
            .OfType<AItem>()
            .ToList();
            return _all;
        }
    }

    public static readonly ShoeLackCream BasicLack = new(1, "Basic Lack", "Simple shoe lack for improving your steps", 0.1f);
    public static readonly ShoeLackCream AdvancedLack = new(2, "Advanced Lack", "Better shoe lack for improving your steps even further", 0.25f);
    public static readonly QuestItem GoldenKey = new(3, "Golden Key", "A Key for a specific door in the city");


    public static AItem GetItembyId(int providedId)
    {
     return ALL.First(i => i.id == providedId);
     
    }

}