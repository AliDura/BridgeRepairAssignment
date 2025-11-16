using BridgeRepair.Models;
using System.Collections.Generic;

namespace BridgeRepair.Solvers;

public class DfsSolver : IBridgeRepairSolver
{
    public string Name => "DFS (Depth-First Search)";

    public bool CanSolve(Equation equation)
    {
        if (equation.Numbers.Length < 2)
            return false;

        var stack = new Stack<(int Index, long CurrentValue)>();
        stack.Push((0, equation.Numbers[0]));

        while (stack.Count > 0)
        {
            var (index, currentValue) = stack.Pop();

            // If we've processed all numbers, check if we reached the target
            if (index == equation.Numbers.Length - 1)
            {
                if (currentValue == equation.TestValue)
                    return true;
                continue;
            }

            long nextNumber = equation.Numbers[index + 1];

            // Try multiplication first (DFS explores deeper paths first)
            long multiplyResult = currentValue * nextNumber;
            if (multiplyResult <= equation.TestValue)
            {
                stack.Push((index + 1, multiplyResult));
            }

            // Try addition
            long addResult = currentValue + nextNumber;
            if (addResult <= equation.TestValue)
            {
                stack.Push((index + 1, addResult));
            }
        }

        return false;
    }
}

