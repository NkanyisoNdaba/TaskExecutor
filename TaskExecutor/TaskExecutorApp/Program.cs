using TaskExecutor;

namespace TaskExecutorApp
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var parser = new OptionsParser();
            return parser.ParseOptionArguments(args);
        }

    }
}
