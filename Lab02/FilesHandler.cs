namespace Lab02
{
    public class FilesHandler
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;

        public FilesHandler()
        {
            string labFolder = "Lab02";
            string baseDirectory = Directory.GetCurrentDirectory();
            string filesFolder = "files";

            if (baseDirectory.EndsWith(labFolder))
            {
                _inputFilePath = Path.Combine(baseDirectory, filesFolder, "INPUT.TXT");
                _outputFilePath = Path.Combine(baseDirectory, filesFolder, "OUTPUT.TXT");
            }
            else
            {
                _inputFilePath = Path.Combine(baseDirectory, labFolder, filesFolder, "INPUT.TXT");
                _outputFilePath = Path.Combine(baseDirectory, labFolder, filesFolder, "OUTPUT.TXT");
            }
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