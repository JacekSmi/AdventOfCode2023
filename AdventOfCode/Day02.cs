using AoCHelper;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string[] _inputs;

    public record Game(int Id, IList<CubeSet> CubeSets)
    {
        public int GetMaxCubesByColor(string color) => 
            CubeSets.Max(set => set.GetCubeCountByColor(color));
    }

    public record CubeSet(IList<Cube> Cubes, IList<Cube> maxCubes)
    {
        public bool IsPossible() =>Cubes.All(cube => cube.IsPossible(maxCubes));

        public int GetCubeCountByColor(string color)
        {
            var cube =  Cubes.FirstOrDefault(cube => cube.Color == color);
            return cube?.Count ?? 0;
        }
    };
    public record Cube(int Count, string Color)
    {
        public bool IsPossible(IList<Cube> maxCubes)
        {
            var result = Count <= maxCubes.Single(cube => cube.Color == Color).Count;
            return result;
        }
    }

    public Day02()
    {
        _inputs = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var maxCubes = new List<Cube>
        {
            new Cube(12, "red"),
            new Cube(13, "green"),
            new Cube(14, "blue")
        };
        
        var games = ParseInput(_inputs, maxCubes);
        var sum = 0;


        foreach (var game in games)
        {
            if(game.CubeSets.All(set => set.IsPossible()))
                sum += game.Id;
        }

        return new ValueTask<string>(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var maxCubes = new List<Cube>
        {
            new Cube(12, "red"),
            new Cube(13, "green"),
            new Cube(14, "blue")
        };
        
        var games = ParseInput(_inputs, maxCubes);
        var sum = 0;
        foreach (var game in games)
        {
            var maxRed = game.GetMaxCubesByColor("red");
            var maxGreen = game.GetMaxCubesByColor("green");
            var maxBlue = game.GetMaxCubesByColor("blue");
            sum += (maxRed * maxGreen * maxBlue);
        }

        return new ValueTask<string>(sum.ToString());
    }

    private IList<Game> ParseInput(string[] inputs, List<Cube> maxCubes)
    {
        var gameList = new List<Game>();
        foreach (var input in inputs)
        {
            var gameId = ParseGameId(input.Split(':').First());

            var cubeSets = ParseCubeSets(input.Split(":", 
                StringSplitOptions.TrimEntries).Last(), maxCubes);

            var game = new Game(gameId, cubeSets);
            gameList.Add(game);
        }

        return gameList;
    }

    private IList<CubeSet> ParseCubeSets(string cubeSetsString, List<Cube> maxCubes)
    {
        var cubeSets = new List<CubeSet>();
        
        foreach (var cubeSetString in cubeSetsString.Split(";", 
                     StringSplitOptions.TrimEntries))
        {
            var cubeSet = ParseCubeSet(cubeSetString, maxCubes);
            cubeSets.Add(cubeSet);
        }

        return cubeSets;
    }

    private CubeSet ParseCubeSet(string cubeSetString, List<Cube> maxCubes)
    {
        var cubes = new List<Cube>();

        foreach (var cubeString in cubeSetString.Split(',', StringSplitOptions.TrimEntries))
        {
            var cube = ParseCube(cubeString);
            cubes.Add(cube);
        }

        return new CubeSet(cubes, maxCubes);
    }

    private Cube ParseCube(string cubeString)
    {
        var count = int.Parse(cubeString.Split(' ', StringSplitOptions.TrimEntries)[0]);
        var color = cubeString.Split(' ', StringSplitOptions.TrimEntries)[1];
        return new Cube(count, color);
    }

    private int ParseGameId(string gameString)
    {
        var id = gameString.Replace("Game ", string.Empty);
        return int.Parse(id);
    }
}