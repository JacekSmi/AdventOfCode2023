using AoCHelper;

namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;
    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
       
    }
    public override ValueTask<string> Solve_1()
    {
        var validDigit = new Dictionary<string, int>
        {
            { "1",     1 },
            { "2",     2 },
            { "3",     3 },
            { "4",     4 },
            { "5",     5 },
            { "6",     6 },
            { "7",     7 },
            { "8",     8 },
            { "9",     9 },
        };
        
        var result = _input.Sum(i => GetCalibrationValue(i, validDigit)).ToString();
        return new(result);
    }

    public override ValueTask<string> Solve_2()
    {
        var validDigit = new Dictionary<string, int>
        {
            { "1",     1 },
            { "2",     2 },
            { "3",     3 },
            { "4",     4 },
            { "5",     5 },
            { "6",     6 },
            { "7",     7 },
            { "8",     8 },
            { "9",     9 },
            { "one",   1 },
            { "two",   2 },
            { "three", 3 },
            { "four",  4 },
            { "five",  5 },
            { "six",   6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine",  9 },
        };
        
        var result = _input.Sum(i => GetCalibrationValue(i, validDigit)).ToString();
        return new(result);
    }

    private static readonly int NO_MATCH = -1;
    private static int GetCalibrationValue(string input, Dictionary<string, int> validDigit)
    {
        int firstValue = validDigit
            .Select(vm => (vm.Value, Position: input.IndexOf(vm.Key)))
            .OrderBy(m => m.Position)
            .First(m => m.Position != NO_MATCH)
            .Value;

        int lastValue = validDigit
            .Select(vm => (vm.Value, Position: input.LastIndexOf(vm.Key)))
            .OrderByDescending(m => m.Position)
            .First(m => m.Position != NO_MATCH)
            .Value;

        return (firstValue * 10) + lastValue;
    }
}