namespace VShevchenko
{
    public class ProcedureChecker
    {
        private Dictionary<string, List<string>> procedures; //[procedure_name : [procedures that calls] ]
        private HashSet<string> visited;
        private HashSet<string> stack;
        private Dictionary<string, bool> results; // [procedure_name : true/false ]

        public ProcedureChecker()
        {
            procedures = new Dictionary<string, List<string>>();
            visited = new HashSet<string>();
            stack = new HashSet<string>();
            results = new Dictionary<string, bool>();
        }

        public void dataChecker(string data, out int result, string message)
        {
            if (!int.TryParse(data, out result))
            {
                throw new FormatException(message);
            }
        }
        public int starsChecker(string data, int index)
        {
            if (data == "*****")
            {
                return index + 1; // skip "*****"
            }
            return index; 
        }

        public void ProcessProcedures(List<string> data)
        {
            int n;

            dataChecker(
                data[0], 
                out n, 
                "The first line must contain a valid integer for the number of procedures."
                );

            int index = 1; // 0 - n-numbers of procedures
            
            for (int i = 0; i < n; i++)
            {
                string procedureName = data[index++];
                Console.WriteLine($"Processing procedure: {procedureName}");

                int k;

                dataChecker(
                    data[index++], 
                    out k, 
                    $"The line {index} must contain a valid integer for the number of calls for procedure {procedureName}."
                    );

                List<string> calls = new List<string>();
                for (int j = 0; j < k; j++)
                {
                    calls.Add(data[index++]);
                }

                procedures[procedureName] = calls;
                results[procedureName] = false;

                index = starsChecker(data[index], index);
            }

            foreach (var procedure in procedures.Keys)
            {
                Console.WriteLine();
                Console.WriteLine($"Visiting procedure: {procedure}");
                visited.Clear();
                stack.Clear();
                IsPotentiallyRecursive(procedure);
            }
        }

        private bool IsPotentiallyRecursive(string procedure)
        {
            if (stack.Contains(procedure))
            {
                results[procedure] = true;
                return true;
            }

            if (visited.Contains(procedure))
            {
                return results[procedure];
            }

            visited.Add(procedure);
            stack.Add(procedure);

            foreach (var calledProcedure in procedures[procedure])
            {
                Console.WriteLine($"Calling procedure: {calledProcedure} from {procedure}");
                IsPotentiallyRecursive(calledProcedure);
            }

            stack.Remove(procedure);
            Console.WriteLine($"Finished checking procedure: {procedure}. Result: {results[procedure]}");
            return results[procedure];
        }

        public List<string> GetResults()
        {
            List<string> output = new List<string>();
            foreach (var procedure in procedures.Keys)
            {
                output.Add(results[procedure] ? "YES" : "NO");
            }
            return output;
        }
    }
}
