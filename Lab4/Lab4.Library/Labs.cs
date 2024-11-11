using System;
using System.Diagnostics;
using System.IO;

namespace Lab4.Library
{
    public class Labs
    {
        private readonly string _baseDirectory;

        public Labs()
        {
            _baseDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\"));
        }

        public void RunLab(string labName, string inputFilePath, string outputFilePath)
        {
            try
            {
                string labDirectory = Path.Combine(_baseDirectory, labName);
                string filesDirectory = CreateLabDirectories(labDirectory, labName);

                CopyInputFile(inputFilePath, filesDirectory);

                LabExecution.ExecuteLabCommand(labName, _baseDirectory);

                CopyOutputFile(outputFilePath, filesDirectory);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running {labName}: {ex.Message}");
            }
        }

        private string CreateLabDirectories(string labDirectory, string labName)
        {
            string labFolder = Path.Combine(labDirectory, labName);
            string filesDirectory = Path.Combine(labFolder, "files");

            if (!Directory.Exists(filesDirectory))
            {
                Directory.CreateDirectory(filesDirectory);
            }

            return filesDirectory;
        }

        private void CopyInputFile(string inputFilePath, string filesDirectory)
        {
            if (!string.IsNullOrEmpty(inputFilePath))
            {
                string destinationInput = Path.Combine(filesDirectory, "INPUT.txt");
                if (File.Exists(destinationInput))
                {
                    File.Delete(destinationInput);
                }
                File.Copy(inputFilePath, destinationInput);
                Console.WriteLine($"Input file copied to: {destinationInput}");
            }
        }

        private void CopyOutputFile(string outputFilePath, string filesDirectory)
        {
            if (!string.IsNullOrEmpty(outputFilePath))
            {
                string sourceOutput = Path.Combine(filesDirectory, "OUTPUT.txt");

                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }
                File.Copy(sourceOutput, outputFilePath);
                Console.WriteLine($"Results written to: {outputFilePath}");
            }
        }
    }
}