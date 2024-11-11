using Lab4.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Commands
{
    public class RunLabCommand : ICommand
    {
        private readonly string[] _args;
        private readonly Labs _labs;

        public RunLabCommand(string[] args, Labs labs)
        {
            _args = args;
            _labs = labs;
        }

        public void Execute()
        {
            if (_args.Length < 2)
            {
                Console.WriteLine("Error: Lab name required (lab1, lab2, or lab3)");
                return;
            }

            string labName = _args[1].ToLower();
            if (!new[] { "lab1", "lab2", "lab3" }.Contains(labName))
            {
                Console.WriteLine("Error: Invalid lab name. Use lab1, lab2, or lab3");
                return;
            }

            var (inputPath, outputPath) = FilePathResolver.ParseFileArguments(_args);
            if (inputPath == null)
            {
                Console.WriteLine("Error: Input file path could not be resolved.");
                return;
            }

            _labs.RunLab(labName.ToUpper(), inputPath, outputPath);
        }
    }
}
