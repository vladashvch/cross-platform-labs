using System;
using System.IO;
using Lab4.Commands;
using Lab4.Library;

class Program
{
    private static readonly Dictionary<string, Func<string[], Labs, ICommand>> Commands =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["version"] = (_, __) => new VersionCommand(),
            ["run"] = (args, labs) => new RunLabCommand(args, labs),
            ["set-path"] = (args, _) => new SetPathCommand(args),
            ["exit"] = (_, __) => new ExitCommand()
        };

    static void Main(string[] args)
    {
        var labs = new Labs();

        if (args.Length > 0)
        {
            ProcessCommand(args, labs);
            return;
        }

        RunInteractiveMode(labs);
    }

    private static void RunInteractiveMode(Labs labs)
    {
        while (true)
        {
            ShowHelp();
            var currentArgs = GetUserInput();
            if (currentArgs.Length == 0) continue;
            ProcessCommand(currentArgs, labs);
        }
    }

    private static void ProcessCommand(string[] args, Labs labs)
    {
        if (!Commands.TryGetValue(args[0], out var commandFactory))
        {
            Console.WriteLine("Unknown command. Use 'help' to see available commands.");
            return;
        }

        var command = commandFactory(args, labs);
        command.Execute();
    }

    private static string[] GetUserInput()
    {
        Console.Write("Input a command: ");
        string userInput = Console.ReadLine();
        return string.IsNullOrWhiteSpace(userInput)
            ? []
            : userInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }

    private static void ShowHelp()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("  version                          - Show program information");
        Console.WriteLine("  run <lab1|lab2|lab3>             - Run specified lab");
        Console.WriteLine("         -i|--input <path>         - Optional input file path");
        Console.WriteLine("         -o|--output <path>        - Optional output file path");
        Console.WriteLine("  set-path -p|--path <path>        - Set path for input/output files");
        Console.WriteLine("  exit                             - Exit the program");
    }
}