using VShevchenko;

namespace Lab3
{
    public static class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("Lab-03");
                Console.WriteLine();
                ExecuteProgram();

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        public static void ExecuteProgram()
        {
            var filesHandler = new FilesHandler();

            string inputFilePath = filesHandler.InputFilePath;
            string outputFilePath = filesHandler.OutputFilePath;

            List<string> data = filesHandler.ProcessInputFile(inputFilePath);
            Console.WriteLine("Data from file:");
            Console.WriteLine(string.Join(Environment.NewLine, data));

            var checker = new ProcedureChecker();
            checker.ProcessProcedures(data);

            List<string> results = checker.GetResults();
            Console.WriteLine("Result:");
            Console.WriteLine(string.Join(Environment.NewLine, results));

            
            File.WriteAllLines(outputFilePath, results);
        }
    }
}