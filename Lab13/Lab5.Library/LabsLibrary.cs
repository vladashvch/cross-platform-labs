using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Lab5.Library
{
    public class LabsLibrary
    {
        private string _inputFilePath;
        private string _outputFilePath;
        private string _lab;

        public LabsLibrary(string lab)
        {
            _lab = lab;
            string currentDir = Directory.GetCurrentDirectory();
            string filesFolder = Path.Combine(currentDir, "..", "..", _lab, _lab, "files");

            _inputFilePath = Path.Combine(filesFolder, "INPUT.TXT");
            _outputFilePath = Path.Combine(filesFolder, "OUTPUT.TXT");
        }

        private void WriteToFile(string data, string file)
        {
            try
            {
                File.WriteAllText(_inputFilePath, data);
                Console.WriteLine($"Дані успішно записані у {Path.GetFileName(_inputFilePath)}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка при запису у {Path.GetFileName(_inputFilePath)}: {ex.Message}");
            }
        }

        public void WriteToInputFile(string data)
        {
            WriteToFile(data, _inputFilePath);
        }

        public void WriteToOutputFile(string data)
        {
            WriteToFile(data, _inputFilePath);
        }

        public void StartLab()
        {
            string currentDir = Directory.GetCurrentDirectory();
            string targetDirectory = Path.Combine(currentDir, "..", "..");

            ExecuteCommand($"dotnet build Build.proj -p:Solution={_lab} -t:Build", targetDirectory);
            ExecuteCommand($"dotnet build Build.proj -p:Solution={_lab} -t:Run", targetDirectory);
        }

        public string ReadOutputFile()
        {
            try
            {
                string data = File.ReadAllText(_outputFilePath);
                Console.WriteLine($"Дані з {Path.GetFileName(_outputFilePath)} успішно прочитані.");
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка при читанні з {Path.GetFileName(_outputFilePath)}: {ex.Message}");
                return string.Empty;
            }
        }

        private void ExecuteCommand(string command, string workingDirectory)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C {command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = workingDirectory
                };

                using (Process process = new Process())
                {
                    process.StartInfo = processStartInfo;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    Console.WriteLine($"Output: {output}");
                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine($"Error: {error}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка при виконанні команди: {ex.Message}");
            }
        }
    }
}
