using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public static class FilePathResolver
    {
        public static (string inputPath, string outputPath) ParseFileArguments(string[] args)
        {
            var paths = ParseCommandLineArgs(args);
            return paths.inputPath == null && paths.outputPath == null
                ? ResolveDefaultPaths()
                : paths;
        }

        private static (string inputPath, string outputPath) ParseCommandLineArgs(string[] args)
        {
            string inputPath = null;
            string outputPath = null;

            for (int i = 2; i < args.Length; i++)
            {
                if (IsInputFlag(args[i]) && i + 1 < args.Length)
                    inputPath = args[++i];
                else if (IsOutputFlag(args[i]) && i + 1 < args.Length)
                    outputPath = args[++i];
            }

            return (inputPath, outputPath);
        }

        private static bool IsInputFlag(string arg) => arg == "-i" || arg == "--input";
        private static bool IsOutputFlag(string arg) => arg == "-o" || arg == "--output";

        private static (string inputPath, string outputPath) ResolveDefaultPaths()
        {
            string labPath = Environment.GetEnvironmentVariable("LAB_PATH");
            if (TryResolvePaths(labPath, out var paths))
                return paths;

            string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (TryResolvePaths(homeDir, out paths))
                return paths;

            Console.WriteLine("Error: INPUT.txt not found in any of the specified locations");
            return (null, null);
        }

        private static bool TryResolvePaths(string basePath, out (string inputPath, string outputPath) paths)
        {
            paths = default;
            if (string.IsNullOrEmpty(basePath) || !Directory.Exists(basePath))
                return false;

            string inputFile = Path.Combine(basePath, "INPUT.txt");
            if (!File.Exists(inputFile))
                return false;

            paths = (inputFile, Path.Combine(basePath, "OUTPUT.txt"));
            return true;
        }
    }
}
