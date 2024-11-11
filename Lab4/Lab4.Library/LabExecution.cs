using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Library
{
    public class LabExecution
    {

        public static void ExecuteLabCommand(string labName, string _baseDirectory)
        {
            try
            {
                string projectFilePath = Path.Combine(_baseDirectory, "Build.proj");
                if (!File.Exists(projectFilePath))
                {
                    throw new FileNotFoundException($"Build project file not found at: {projectFilePath}");
                }

                Process process = InputCommand(labName, _baseDirectory);
                OutputCommand(process);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing lab: {ex.Message}");
            }
        }

        private static Process InputCommand(string labName, string _baseDirectory)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"build Build.proj -p:Solution={labName} -t:Run", 
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = _baseDirectory
                }
            };

            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    Console.WriteLine(e.Data);
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    Console.WriteLine($"Error: {e.Data}");
                }
            };

            return process;
        }
        private static void OutputCommand(Process process)
        {
            try
            {
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit(); 

                if (process.ExitCode != 0)
                {
                    throw new Exception($"Lab execution failed with exit code {process.ExitCode}");
                }

                Console.WriteLine("Lab execution completed successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute lab command: {ex.Message}");
            }
        }
    }
}
