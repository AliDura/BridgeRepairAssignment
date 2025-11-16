using BridgeRepair.Models;
using System.Collections.Generic;

namespace BridgeRepair.Solvers;

public class BfsSolver : IBridgeRepairSolver
{
    public string Name => "BFS (Breadth-First Search)";

    public bool CanSolve(Equation equation)
    {
        if (equation.Numbers.Length < 2)
            return false;

        var queue = new Queue<(int Index, long CurrentValue)>();
        queue.Enqueue((0, equation.Numbers[0]));

        while (queue.Count > 0)
        {
            var (index, currentValue) = queue.Dequeue();

            // If we've processed all numbers, check if we reached the target
            if (index == equation.Numbers.Length - 1)
            {
                if (currentValue == equation.TestValue)
                    return true;
                continue;
            }

            long nextNumber = equation.Numbers[index + 1];

            // Try addition
            long addResult = currentValue + nextNumber;
            if (addResult <= equation.TestValue)
            {
                queue.Enqueue((index + 1, addResult));
            }

            // Try multiplication
            long multiplyResult = currentValue * nextNumber;
            if (multiplyResult <= equation.TestValue)
            {
                queue.Enqueue((index + 1, multiplyResult));
            }
        }

        return false;
    }
}

