using System.Numerics;

public class GameMap
{
    private const ConsoleColor EMPTY_COLOR = ConsoleColor.Black;
    private const string EMPTY_SYMBOL = " \u25FC";
    private const ConsoleColor BORDER_COLOR = ConsoleColor.White;
    private const string BORDER_SYMBOL = " \u25AA";
    private const string LOCATION_SYMBOL = " \u25C8";
    private const ConsoleColor WAY_COLOR = ConsoleColor.DarkYellow;
    private const string WAY_SYMBOL = " \u25AA";
    private const string DIRECTION_SYMBOL_UP = " \u2B61";
    private const string DIRECTION_SYMBOL_UPRIGHT = " \u2B67";
    private const string DIRECTION_SYMBOL_RIGHT = " \u2B62";
    private const string DIRECTION_SYMBOL_DOWNRIGHT = " \u2B68";
    private const string DIRECTION_SYMBOL_DOWN = " \u2B63";
    private const string DIRECTION_SYMBOL_DOWNLEFT = " \u2B69";
    private const string DIRECTION_SYMBOL_LEFT = " \u2B60";
    private const string DIRECTION_SYMBOL_UPLEFT = " \u2B66";


    public List<Location> nodes;
    private List<Trail> edges;

    private Vector2 mapSize;

    /// <summary>
    /// Contains the symbol and color at a certain position.
    /// </summary>
    private Tuple<string, ConsoleColor>[,] mapSymbols;

    private Player player;


    public GameMap(Player player)
    {
        nodes = new List<Location>();
        edges = new List<Trail>();
        this.player = player;
        CreateMap();

        // setup data to draw the map
        mapSize = DetermineMapBoundaries();

        mapSymbols = new Tuple<string, ConsoleColor>[(int)mapSize.X, (int)mapSize.Y];
        FillMapSymbols();
    }

    public void CreateMap()
    {

        Location cityCentre = CreateNode(12, 12, ConsoleColor.Blue, "City Centre", "The main square of a large city");
        Location cityOutskirts = CreateNode(12, 14, ConsoleColor.Cyan, "City Outskirts", "The quiet and peacefull suburbs of the city");
        Location forest = CreateNode(14, 16, ConsoleColor.Green, "Forest", "A dark forest filled with trees");
        Location mountains = CreateNode(11, 18, ConsoleColor.Gray, "Mountains", "Large mountain peaks with a cold climate surrounding them");
        Location mountainTop = CreateNode(12, 18, ConsoleColor.DarkGray, "Mountaintop", "Standing on the peak of the mountain range, you can see as far as never before");
        Location coast = CreateNode(9, 20, ConsoleColor.Yellow, "Coast", "Vast coastline seperating the land from the endless sea");
        Location desert = CreateNode(7, 18, ConsoleColor.DarkYellow, "Desert", "The heat of this sandy desert is a challange for many");

        CreatEdge(cityCentre, forest, true, 10);
        CreatEdge(cityCentre, mountains, false, 30);
        CreatEdge(cityCentre, coast, false, 60);
        CreatEdge(forest, mountains, true, 20);
        CreatEdge(coast, forest, true, 50);
        CreatEdge(mountains, coast, true, 30);
        CreatEdge(cityCentre, cityOutskirts, true, 5);
        CreatEdge(forest, cityOutskirts, true, 5);
        CreatEdge(cityCentre, desert, false, 50);
        CreatEdge(coast, desert, true, 10);
        CreatEdge(mountains, mountainTop, true, 5);




        MarathonEvent marathonEvent = new MarathonEvent(player);
        marathonEvent.AddCondition(new ByChanceCondition(0.25f));
        forest.AddEvent(marathonEvent);

        DesertDamageEvent desertDamageEvent = new DesertDamageEvent(player);
        desert.AddEvent(desertDamageEvent);

        RunningBootsEvent runningBootsEvent = new RunningBootsEvent(player);
        runningBootsEvent.AddCondition(new ByChanceCondition(0.5f));
        runningBootsEvent.AddCondition(new StepCountCondition(player, 500));
        cityCentre.AddEvent(runningBootsEvent);
        cityOutskirts.AddEvent(runningBootsEvent);

        ToCityTeleportEvent toCityTeleportEvent = new ToCityTeleportEvent(player);
        toCityTeleportEvent.AddCondition(new MilestoneCompletionCondition(player, Milestone.EVERYTHING_REVEALED, Milestone.FIRST_MAP_USAGE));
        toCityTeleportEvent.AddCondition(new ByStepFactorCondition(player, 1.5f));
        coast.AddEvent(toCityTeleportEvent);
        mountains.AddEvent(toCityTeleportEvent);

        mountainTop.AddEvent(new LocationRevealEvent(player, coast)
                                    .AddText("You see a path to a coast in the distance.")
                                    .AddByChanceCondition(0.5f));
    }

    public Location CreateNode(int xPos, int yPos, ConsoleColor color, string name, string description)
    {
        Location node = new Location(xPos, yPos, color, name, description);
        nodes.Add(node);
        return node;
    }

    public void CreatEdge(Location startNode, Location destinationNode, bool biDirectional, int stepValue)
    {
        Trail edge = new Trail(startNode, destinationNode, biDirectional, stepValue);
        edges.Add(edge);
    }

    public List<Trail> GetPaths()
    {
        List<Trail> paths = new List<Trail>();
        foreach (Trail edge in edges)
        {
            if (edge.startNode == GetCurrentLocation() || (edge.destinationNode == GetCurrentLocation() && edge.biDirectional))
            {
                paths.Add(edge);
            }

        }
        return paths;
    }

    /// <summary>
    /// This function is responsible to caluclate the boundaries of the map according to the given nodes and their positions.
    /// 
    /// The map is always a little bit bigger than the actual nodes.
    /// </summary>
    /// <returns></returns>
    private Vector2 DetermineMapBoundaries()
    {
        if (nodes == null) return new Vector2(0, 0);

        int maxX = nodes.Max(node => node.xPos);
        int maxY = nodes.Max(node => node.yPos);

        return new Vector2(maxX + 5, maxY + 5);
    }

    /// <summary>
    /// This function fills the double array with their according symbols.
    /// </summary>
    private void FillMapSymbols()
    {
        CreateGameMap();
        FillEdgeSymbols();
        FillLocationSymbols();
    }

    /// <summary>
    ///  Draws the map.
    /// </summary>
    public void DrawMap()
    {
        for (int y = 0; y < mapSize.Y; y++)
        {
            for (int x = 0; x < mapSize.X; x++)
            {
                Tuple<string, ConsoleColor> value = mapSymbols[x, y];
                Console.ForegroundColor = value.Item2;

                Console.Write(value.Item1);

                Console.ResetColor();
            }

            if (y == 1)
            {
                Console.Write(" Legende");
            }
            else if (y == 2)
            {
                Console.Write(" --------");
            }
            else if (y > 2 && y <= nodes.Count + 2)
            {
                DrawLegendEntry(nodes[y - 3]);
            }

            RPGWriter.LineBreak();
        }
    }

    private void DrawLegendEntry(Location node)
    {
        Console.Write("");
        Console.ForegroundColor = node.color;
        Console.Write(LOCATION_SYMBOL);
        Console.ResetColor();
        Console.Write(" ");
        Console.Write(node.name);
    }

    private void PlaceWay(int x, int y)
    {
        if (mapSymbols[x, y].Item1 == EMPTY_SYMBOL)
        {
            mapSymbols[x, y] = new Tuple<string, ConsoleColor>(WAY_SYMBOL, WAY_COLOR);
        }
    }

    private string IsAdjacent(int pointX, int pointY, int nebX, int nebY)
    {
        if (nebX == pointX && nebY == pointY - 1)
        {
            return DIRECTION_SYMBOL_DOWN;
        }
        else if (nebX == pointX && nebY == pointY + 1)
        {
            return DIRECTION_SYMBOL_UP;
        }
        else if (nebX == pointX + 1 && nebY == pointY)
        {
            return DIRECTION_SYMBOL_LEFT;
        }
        else if (nebX == pointX - 1 && nebY == pointY)
        {
            return DIRECTION_SYMBOL_RIGHT;
        }
        else if (nebX == pointX + 1 && nebY == pointY + 1)
        {
            return DIRECTION_SYMBOL_UPLEFT;
        }
        else if (nebX == pointX + 1 && nebY == pointY - 1)
        {
            return DIRECTION_SYMBOL_DOWNLEFT;
        }
        else if (nebX == pointX - 1 && nebY == pointY + 1)
        {
            return DIRECTION_SYMBOL_UPRIGHT;
        }
        else if (nebX == pointX + 1 && nebY == pointY + 1)
        {
            return DIRECTION_SYMBOL_DOWNRIGHT;
        }
        else
        {
            return string.Empty;
        }
    }

    private void CreateGameMap()
    {
        for (int y = 0; y < mapSize.Y; y++)
        {
            for (int x = 0; x < mapSize.X; x++)
            {
                if (y == 0 || y == mapSize.Y - 1 || x == 0 || x == mapSize.X - 1)
                {
                    mapSymbols[x, y] = new Tuple<string, ConsoleColor>(BORDER_SYMBOL, BORDER_COLOR);
                }
                else
                {
                    mapSymbols[x, y] = new Tuple<string, ConsoleColor>(EMPTY_SYMBOL, EMPTY_COLOR);
                }
            }
        }
    }

    private void FillEdgeSymbols()
    {
        foreach (Trail edge in edges)
        {
            // Draw path
            int fromX = edge.startNode.xPos;
            int toX = edge.destinationNode.xPos;
            int fromY = edge.startNode.yPos;
            int toY = edge.destinationNode.yPos;

            // multiple cases
            int distanceX = Math.Abs(fromX - toX);
            int distanceY = Math.Abs(fromY - toY);

            // number of diagonal steps
            int diagonalWays = Math.Min(distanceX, distanceY);

            // number of linear steps
            int linearSteps = Math.Max(distanceX, distanceY) - diagonalWays;

            int currentX = fromX;
            int currentY = fromY;
            for (int i = 0; i < diagonalWays; i++)
            {
                currentX = fromX < toX ? currentX + 1 : currentX - 1;
                currentY = fromY < toY ? currentY + 1 : currentY - 1;
                string currentSymbol = IsAdjacent(toX, toY, currentX, currentY);
                if (!string.IsNullOrEmpty(currentSymbol))
                {
                    mapSymbols[currentX, currentY] = new Tuple<string, ConsoleColor>(currentSymbol, WAY_COLOR);
                    continue;
                }
                PlaceWay(currentX, currentY);
            }

            for (int i = 0; i < linearSteps; i++)
            {
                if (currentX == toX)
                {
                    // make vertical
                    currentY = fromY < toY ? currentY + 1 : currentY - 1;
                }
                else if (currentY == toY)
                {
                    // make horizontal
                    currentX = fromX < toX ? currentX + 1 : currentX - 1;
                }

                string currentSymbol = IsAdjacent(toX, toY, currentX, currentY);
                if (!string.IsNullOrEmpty(currentSymbol))
                {
                    mapSymbols[currentX, currentY] = new Tuple<string, ConsoleColor>(currentSymbol, WAY_COLOR);
                    continue;
                }

                PlaceWay(currentX, currentY);
            }


        }
    }

    private void FillLocationSymbols()
    {
        foreach (Location node in nodes)
        {
            mapSymbols[node.xPos, node.yPos] = new Tuple<string, ConsoleColor>(LOCATION_SYMBOL, node.color);
        }
    }

    public void SetCurrentLocation(Location l)
    {
        player.currentLocationName = l.name;
        player.RevealLocation(l);
    }

    public Location GetCurrentLocation()
    {
        return nodes.First(l => l.name == player.currentLocationName);
    }

    public Location GetStartLocation()
    {
        return nodes[0];
    }
}