namespace Lab4.Commands
{

    public class SetPathCommand : ICommand
    {
        private readonly string[] _args;

        public SetPathCommand(string[] args)
        {
            _args = args;
        }

        public void Execute()
        {
            if (_args.Length < 3 || _args[1] != "-p" && _args[1] != "--path")
            {
                Console.WriteLine("Error: Path required. Use: set-path -p <path> or set-path --path <path>");
                return;
            }

            string path = _args[2];
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"Error: Directory does not exist: {path}");
                return;
            }

            Environment.SetEnvironmentVariable("LAB_PATH", path);
            Console.WriteLine($"Path set to: {path}");
        }
    }
}
