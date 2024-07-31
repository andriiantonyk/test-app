namespace LogSearchConsole.Services.Interfaces
{
    public interface ILogSearchService
    {
        IEnumerable<LogSearchResult> SearchLogs(string pattern);
    }
}
