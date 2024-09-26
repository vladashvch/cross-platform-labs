

namespace Lab02;

public static class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Lab-02");
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

        List<int> answers = ProcessInputFile(inputFilePath);

        File.WriteAllText(outputFilePath, string.Join(Environment.NewLine, answers).Trim());
    }

    public static List<int> ProcessInputFile(string inputFilePath)
    {
        List<int> answers = new List<int>();

        string[] lines = File.ReadAllLines(inputFilePath);

        foreach (string line in lines)
        {
            int N = int.Parse(line.Trim());
            if (FilesHandler.IsValuesValid(N))
            {
                int result = StartProgram(N);
                answers.Add(result);
            }
        }

        return answers;
    }

    public static int StartProgram(int N)
    {
        bool[] a = new bool[1001];
        int[,,] d = new int[10011, 10, 10];

        for (int i = 100; i < 1000; i++)
        {
            a[i] = IsPrimeNumber(i);
        }

        FindThreeDigitPrimeNumbers(a, d);
        MoreThreeArray(a, d, N);

        return CountEnount(d, N);
    }

    public static bool IsPrimeNumber(int N)
    {
        if (N <= 1) return false;
        if (N <= 3) return true;
        if (N % 2 == 0 || N % 3 == 0) return false;

        for (int i = 5; i * i <= N; i += 6)
        {
            // Check for multiplicity of numbers of the form 6k±1
            // (which is an optimized method of checking for simplicity)
            if (N % i == 0 || N % (i + 2) == 0) return false;
        }
        return true;
    }

    public static int MathMod(int expression, int MOD = 1000000009)
    {
        return expression % MOD;
    }

    public static int CountEnount(int[,,] d, int N)
    {
        int answer = 0;
        Console.WriteLine($"Starting count of N-digit three prime numbers... result = 0");

        for (int position1 = 0; position1 < 10; position1++)
        {
            for (int position2 = 0; position2 < 10; position2++)
            {
                answer += d[N, position1, position2];
                Console.WriteLine($"+ positions [{position1}, {position2}]: {d[N, position1, position2]} => total: {answer}");
            }
        }

        Console.WriteLine($"Final count of {N}-digit three prime numbers: {answer}");
        Console.WriteLine();
        return answer;
    }

    public static void FindThreeDigitPrimeNumbers(bool[] a, int[,,] d)
    {
        Console.WriteLine("Identifying three-digit prime numbers (N=3):");

        for (int position1 = 0; position1 < 10; position1++)
        {
            for (int position2 = 0; position2 < 10; position2++)
            {
                for (int p3 = 0; p3 < 10; p3++)
                {
                    int number = position1 * 100 + position2 * 10 + p3;
                    if (a[number])
                    {
                        d[3, p3, position2]++;
                        Console.WriteLine($"Found three-digit prime: {number}");
                    }
                }
            }
        }

        Console.WriteLine();
    }

    static void MoreThreeArray(bool[] a, int[,,] d, int N)
    {
        Console.WriteLine("Calculating for N-digit three prime numbers (N >= 4):");

        for (int i = 4; i <= N; i++)
        {
            for (int p1 = 0; p1 < 10; p1++)
            {
                for (int p2 = 0; p2 < 10; p2++)
                {
                    for (int p3 = 0; p3 < 10; p3++)
                    {
                        int number = p1 * 100 + p2 * 10 + p3;
                        if (a[number])
                        {
                            d[i, p3, p2] = MathMod(d[i, p3, p2] + d[i - 1, p2, p1]);
                            Console.WriteLine($"Updated d[{i}, {p3}, {p2}] = {d[i, p3, p2]} (from d[{i - 1}, {p2}, {p1}])");
                        }
                    }
                }
            }
        }

        Console.WriteLine();
    }
}