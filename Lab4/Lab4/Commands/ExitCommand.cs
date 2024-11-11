namespace Lab4.Commands
{
    public class ExitCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Exiting the program...");
            Environment.Exit(0);
        }
    }
}
