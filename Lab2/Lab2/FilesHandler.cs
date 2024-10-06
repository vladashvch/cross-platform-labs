namespace Lab2
{
    public class FilesHandler
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;

        public FilesHandler()
        {
            string labFolder = "Lab2";
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

        public static bool IsValuesValid(int N)
        {
            return 3 <= N && N <= 10000;
        }
    }
}