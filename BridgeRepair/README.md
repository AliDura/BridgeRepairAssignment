# Day 7: Bridge Repair - C# Solution

A C# solution for the "Bridge Repair" puzzle that implements three different algorithmic approaches to solve the calibration equations problem.

## Problem Description

The Historians take you to a rope bridge over a river in the middle of a jungle. A group of engineers are trying to repair it, but young elephants stole all the operators from their calibration equations! 

You need to determine which test values could possibly be produced by placing any combination of operators (`+` and `*`) into their calibration equations.

### Key Rules:
- Operators are evaluated **left-to-right** (no operator precedence)
- Numbers in the equations **cannot be rearranged**
- Only two operators are available: **addition (+)** and **multiplication (*)**
- The goal is to find which equations can be made true and sum their test values

### Example:
```
190: 10 19
```
This can be solved as: `10 * 19 = 190` ✓

```
3267: 81 40 27
```
This can be solved in two ways:
- `81 + 40 * 27 = (81 + 40) * 27 = 121 * 27 = 3267` ✓
- `81 * 40 + 27 = (81 * 40) + 27 = 3240 + 27 = 3267` ✓

```
292: 11 6 16 20
```
This can be solved as: `11 + 6 * 16 + 20 = (11 + 6) * 16 + 20 = 17 * 16 + 20 = 272 + 20 = 292` ✓

## Solution Approaches

This project implements **three different algorithmic approaches** to solve the problem:

### 1. Recursive Solver (`RecursiveSolver.cs`)
- Uses **recursive backtracking** to explore all possible operator combinations
- Recursively tries both addition and multiplication at each position
- Includes early pruning to skip paths that exceed the target value
- **Time Complexity**: O(2^n) where n is the number of operator positions
- **Space Complexity**: O(n) for the recursion stack

### 2. BFS Solver (`BfsSolver.cs`)
- Uses **Breadth-First Search** with a queue
- Explores all possibilities level by level (by operator position)
- Guarantees finding a solution at the earliest possible depth
- **Time Complexity**: O(2^n)
- **Space Complexity**: O(2^n) for the queue in worst case

### 3. DFS Solver (`DfsSolver.cs`)
- Uses **Depth-First Search** with a stack
- Explores paths deeply before backtracking
- More memory-efficient than BFS for deep solutions
- **Time Complexity**: O(2^n)
- **Space Complexity**: O(n) for the stack

All three approaches produce the same results but use different traversal strategies, making this a great educational example of different problem-solving techniques.

## Project Structure

```
BridgeRepair/
├── BridgeRepair.csproj          # Project file
├── Program.cs                    # Main entry point
├── Models/
│   └── Equation.cs              # Equation model and parsing logic
├── Solvers/
│   ├── IBridgeRepairSolver.cs   # Interface for all solvers
│   ├── RecursiveSolver.cs       # Recursive implementation
│   ├── BfsSolver.cs             # Breadth-First Search implementation
│   └── DfsSolver.cs             # Depth-First Search implementation
└── input.txt                     # Input file (place your puzzle input here)
```

## Requirements

- .NET 8.0 SDK or later
- Windows, Linux, or macOS

## Building the Solution

### Build the project:
```bash
dotnet build BridgeRepair.sln
```

### Build in Release mode:
```bash
dotnet build BridgeRepair.sln --configuration Release
```

## Running the Program

### Option 1: Run with automatic file detection
Place your input file named `input.txt` in the `BridgeRepair` folder, then run:
```bash
dotnet run --project BridgeRepair/BridgeRepair.csproj
```

The program will automatically search for `input.txt` in multiple locations:
- Current working directory
- Project directory (BridgeRepair folder)
- Executable directory
- Solution root directory

### Option 2: Specify input file path
```bash
dotnet run --project BridgeRepair/BridgeRepair.csproj input.txt
```

Or with a full path:
```bash
dotnet run --project BridgeRepair/BridgeRepair.csproj "C:\path\to\your\input.txt"
```

### Option 3: Run the compiled executable
After building, you can run the executable directly:
```bash
.\BridgeRepair\bin\Debug\net8.0\BridgeRepair.exe input.txt
```

## Input Format

The input file should contain one equation per line in the following format:
```
<test_value>: <number1> <number2> [<number3> ...]
```

### Example Input:
```
190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20
```

- Each line represents a single equation
- The test value appears before the colon
- Numbers after the colon are separated by spaces
- Empty lines are automatically ignored

## Output

The program processes all equations using all three solvers and displays:

1. **For each solver approach:**
   - List of solvable equations
   - Total calibration result (sum of test values for solvable equations)

2. **Example Output:**
```
=== Day 7: Bridge Repair ===

Reading input from: C:\Learning\CanDidTest\BridgeRepair\input.txt

--- Recursive Approach ---
Solvable equations: 3
  ✓ 190: 10 19
  ✓ 3267: 81 40 27
  ✓ 292: 11 6 16 20
Total Calibration Result: 3749

--- BFS (Breadth-First Search) Approach ---
Solvable equations: 3
  ✓ 190: 10 19
  ✓ 3267: 81 40 27
  ✓ 292: 11 6 16 20
Total Calibration Result: 3749

--- DFS (Depth-First Search) Approach ---
Solvable equations: 3
  ✓ 190: 10 19
  ✓ 3267: 81 40 27
  ✓ 292: 11 6 16 20
Total Calibration Result: 3749
```

## Implementation Details

### Equation Parsing
- The `Equation.Parse()` method handles parsing input lines
- Automatically trims whitespace and handles multiple spaces
- Validates format and converts strings to integers

### Solver Interface
All solvers implement `IBridgeRepairSolver` interface:
```csharp
public interface IBridgeRepairSolver
{
    string Name { get; }
    bool CanSolve(Equation equation);
}
```

### Optimization Features
- **Early pruning**: Skips paths where the current value exceeds the target (since we only use + and *)
- **Long arithmetic**: Uses `long` type to prevent integer overflow
- **Left-to-right evaluation**: Correctly implements the problem's evaluation rules

## Code Style

This project follows .NET and C# best practices:
- PascalCase for classes, methods, and public members
- camelCase for local variables and private fields
- Interface names prefixed with "I"
- Uses C# 10+ features (records, pattern matching, null-coalescing)
- Proper error handling and validation
- Clean separation of concerns (Models, Solvers)


