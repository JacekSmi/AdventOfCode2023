using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string[] _inputs;

    public Day06()
    {
        _inputs = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var score = ParseRace().Select((race, distance) =>
            {
                return Enumerable.Range(0, (int)race.time)
                    .Select(charge => new Boat(charge, race.time - charge))
                    .Count(boat => boat.Distance > race.distance);
            })
            .Aggregate((x,y) => x * y);
        
       

        return new ValueTask<string>(score.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var score = ParseRace().Select((race, distance) =>
            {
                return Enumerable.Range(14, (int)race.time)
                    .Select(charge => new Boat(charge, race.time - charge))
                    .Count(boat => boat.Distance > race.distance);
            })
            .Aggregate((x,y) => x * y);
        
       

        return new ValueTask<string>(score.ToString());
    }

    private IEnumerable<(long time,long distance)> ParseRace()
    {
        var times = _inputs[0]
            .Replace("Time:", string.Empty)
            .Replace(" ", string.Empty)
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse);
       
        var distances = _inputs[1]
            .Replace("Distance:", string.Empty)
            .Replace(" ", string.Empty)
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse);


        return times.Zip(distances, (time, distance) => (time, distance));
    }

    private sealed record Boat(long Speed, long RaceTime)
    {
        public long Distance { get; init; } = Speed * RaceTime;
    };
}