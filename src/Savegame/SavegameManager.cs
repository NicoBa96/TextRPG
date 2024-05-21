using System.Text.Json;

public class SavegameManager
{
    private const string saveDataName = "save.json";

    public static void SaveGame(Player player)
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        string jsonString = JsonSerializer.Serialize(player, options);
        File.WriteAllText(saveDataName, jsonString);
    }

    public static bool HasSaveGame()
    {
        return File.Exists(saveDataName);
    }

    public static Player LoadSaveGame()
    {
        string loadGame = File.ReadAllText(saveDataName);
        Player? player = JsonSerializer.Deserialize<Player>(loadGame);
        if (player == null)
        {
            RPGWriter.Red("Error: Failed to load saved player!");
            Environment.Exit(1);
        }

        return player;
    }

    public static void DeleteSaveGame()
    {
        if (HasSaveGame())
        {
            File.Delete(saveDataName);
        }
    }
}