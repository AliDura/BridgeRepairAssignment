using BridgeRepair.Models;

namespace BridgeRepair.Solvers;

public class RecursiveSolver : IBridgeRepairSolver
{
    public string Name => "Recursive";

    public bool CanSolve(Equation equation)
    {
        if (equation.Numbers.Length < 2)
            return false;

        return CanSolveRecursive(equation.Numbers, 0, equation.Numbers[0], equation.TestValue);
    }

    private bool CanSolveRecursive(long[] numbers, int index, long currentValue, long target)
    {
        // Base case: if we've processed all numbers
        if (index == numbers.Length - 1)
        {
            return currentValue == target;
        }

        long nextNumber = numbers[index + 1];

        // Try addition
        long addResult = currentValue + nextNumber;
        if (addResult <= target && CanSolveRecursive(numbers, index + 1, addResult, target))
        {
            return true;
        }

        // Try multiplication
        long multiplyResult = currentValue * nextNumber;
        if (multiplyResult <= target && CanSolveRecursive(numbers, index + 1, multiplyResult, target))
        {
            return true;
        }

        return false;
    }
}

