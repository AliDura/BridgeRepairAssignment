using BridgeRepair.Models;

namespace BridgeRepair.Solvers;

public interface IBridgeRepairSolver
{
    string Name { get; }
    bool CanSolve(Equation equation);
}

