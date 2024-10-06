namespace Lab3
{
    public class FilesHandler
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;

        public FilesHandler()
        {
            string labFolder = "Lab3";
            string baseDirectory = Directory.GetCurrentDirectory();
            string filesFolder = "files";

            _inputFilePath = Path.Combine(baseDirectory, labFolder, filesFolder, "INPUT.TXT");
            _outputFilePath = Path.Combine(baseDirectory, labFolder, filesFolder, "OUTPUT.TXT");

        }

        public string InputFilePath
        {
            get { return _inputFilePath; }
        }

        public string OutputFilePath
        {
            get { return _outputFilePath; }
        }

        public List<string> ProcessInputFile(string inputFilePath)
        {
            List<string> lines = new List<string>();
            string[] fileLines = File.ReadAllLines(inputFilePath);

            foreach (string line in fileLines)
            {
                lines.Add(line);
            }

            return lines;
        }
    }
}