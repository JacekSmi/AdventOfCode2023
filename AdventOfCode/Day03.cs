using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string[] _inputs;

    private record Symbol(int Line, int Postion, string Value)
    {
        public IEnumerable<Number> GetAdjacentNumbers(Number[] numbers) =>
            numbers.Where(n => n.Line >= Line - 1 &&
                               n.Line <= Line + 1 &&
                               n.End >= Postion - 1 &&
                               n.Start <= Postion + 1);
    }

    private record Number(int Line, int Start, int End, int Value)
    {
        public bool IsAdjacentToSymbol(Symbol[] symbols) =>
            symbols.Any(n => n.Line >= Line - 1 &&
                             n.Line <= Line + 1 &&
                             n.Postion >= Start - 1 &&
                             n.Postion <= End + 1);
    };

    public Day03()
    {
        _inputs = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var (numbers, symbols) = ParseInput();
        var result = numbers
            .Where(number => number.IsAdjacentToSymbol(symbols))
            .Sum(number => number.Value);

        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
       
        var (numbers, symbols) = ParseInput();
        var gearRatio = symbols
            .Where(symbol => symbol.Value == "*")
            .Select(symbol => symbol.GetAdjacentNumbers(numbers))
            .Where(adjactedNumbers => adjactedNumbers.Count() == 2)
            .Select(numList => numList.First().Value * numList.Last().Value)
            .Sum();
            
        
        return new ValueTask<string>(gearRatio.ToString());
    }

    private (Number[] numbers, Symbol[] symbols) ParseInput()
    {
        var lines = _inputs;
        var numbers = new List<Number>();
        var symbols = new List<Symbol>();
        var numberRegex = new Regex(@"\d+");
        var symbolRegex = new Regex(@"[^.\d]");

        for (var i = 0; i < lines.Length; i++)
        {
            foreach (Match match in numberRegex.Matches(lines[i]))
            {
                var numberValue = int.Parse(match.Value);
                var start = match.Index;
                var end = match.Index + match.Length - 1;
                numbers.Add(new Number(i, start, end, numberValue));
            }

            foreach (Match match in symbolRegex.Matches(lines[i]))
            {
                symbols.Add(new Symbol(i, match.Index, match.Value));
            }
        }

        return (numbers.ToArray(), symbols.ToArray());
    }
}