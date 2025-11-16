namespace BridgeRepair.Models;

public record Equation(long TestValue, long[] Numbers)
{
    public static Equation Parse(string line)
    {
        var parts = line.Split(':', StringSplitOptions.TrimEntries);
        var testValue = long.Parse(parts[0]);
        var numbers = parts[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();
        
        return new Equation(testValue, numbers);
    }
}

