using BridgeRepair.Models;
using BridgeRepair.Solvers;

namespace BridgeRepair;

class Program
{
    static void Main(string[] args)
    {
        // Initialize solvers
        var solvers = new IBridgeRepairSolver[]
        {
            new RecursiveSolver(),
            new BfsSolver(),
            new DfsSolver()
        };

        Console.WriteLine("=== Day 7: Bridge Repair ===\n");

        // Determine input file - check multiple locations
        string? inputFile = null;
        
        // Get the directory where the executable/project is located
        var projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var currentDirectory = Directory.GetCurrentDirectory();
        
        // List of directories to search
        var searchDirectories = new[]
        {
            currentDirectory,                    // Current working directory
            projectDirectory,                    // Executable directory
            Path.GetDirectoryName(projectDirectory) ?? currentDirectory, // Parent of executable (for bin/Debug/net8.0)
            Path.Combine(projectDirectory, "..", "..", ".."), // Project root (from bin/Debug/net8.0)
        }.Distinct().ToArray();

        if (args.Length > 0)
        {
            // If path is provided, check if it exists as-is or in search directories
            if (File.Exists(args[0]))
            {
                inputFile = args[0];
            }
            else
            {
                // Try to find it in search directories
                foreach (var dir in searchDirectories)
                {
                    var fullPath = Path.Combine(dir, args[0]);
                    if (File.Exists(fullPath))
                    {
                        inputFile = fullPath;
                        break;
                    }
                }
            }
        }
        else
        {
            // Try default input file names in all search directories
            var defaultFiles = new[] { "input.txt", "input", "data.txt", "data" };
            foreach (var dir in searchDirectories)
            {
                foreach (var file in defaultFiles)
                {
                    var fullPath = Path.Combine(dir, file);
                    if (File.Exists(fullPath))
                    {
                        inputFile = fullPath;
                        break;
                    }
                }
                if (inputFile != null) break;
            }
        }

        // Read and parse equations from file
        if (inputFile != null && File.Exists(inputFile))
        {
            // Normalize the path for display
            var normalizedPath = Path.GetFullPath(inputFile);
            Console.WriteLine($"Reading input from: {normalizedPath}\n");
            var inputLines = File.ReadAllLines(inputFile)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToArray();
            
            if (inputLines.Length == 0)
            {
                Console.WriteLine("Error: Input file is empty.");
                return;
            }

            var equations = inputLines.Select(Equation.Parse).ToList();

            // Process with each solver
            foreach (var solver in solvers)
            {
                Console.WriteLine($"--- {solver.Name} Approach ---");
                var solvableEquations = new List<Equation>();
                var totalCalibration = 0L;

                foreach (var equation in equations)
                {
                    if (solver.CanSolve(equation))
                    {
                        solvableEquations.Add(equation);
                        totalCalibration += equation.TestValue;
                    }
                }

                Console.WriteLine($"Solvable equations: {solvableEquations.Count}");
                foreach (var eq in solvableEquations)
                {
                    Console.WriteLine($"  ✓ {eq.TestValue}: {string.Join(" ", eq.Numbers)}");
                }
                Console.WriteLine($"Total Calibration Result: {totalCalibration}\n");
            }
        }
        else
        {
            Console.WriteLine("Error: Input file not found.");
            Console.WriteLine($"\nSearched in the following directories:");
            foreach (var dir in searchDirectories)
            {
                Console.WriteLine($"  - {dir}");
            }
            Console.WriteLine("\nUsage: BridgeRepair.exe <input_file.txt>");
            Console.WriteLine("Or place your input in a file named 'input.txt' in one of the directories above.");
        }
    }
}

