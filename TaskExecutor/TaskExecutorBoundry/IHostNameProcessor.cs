namespace TaskExecutor
{
    public interface IHostNameProcessor
    {
        string GetComputerName(bool fullyqulified);
        string GetHostName();
        string GetFullyQualifiedHostName();

    }
}