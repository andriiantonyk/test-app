using LogSearchConsole.Services.Interfaces;
using System.Text.RegularExpressions;

namespace LogSearchConsole.Services
{
    public class LogSearchService : ILogSearchService
    {
        private readonly string _directoryPath;

        public LogSearchService(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public IEnumerable<LogSearchResult> SearchLogs(string pattern)
        {
            var regexPattern = WildcardToRegex(pattern);
            var regex = new Regex(regexPattern, RegexOptions.Compiled);

            var results = new List<LogSearchResult>();

            foreach (var filePath in Directory.EnumerateFiles(_directoryPath, "*.log"))
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    string line;
                    int lineNumber = 0;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lineNumber++;
                        if (regex.IsMatch(line))
                        {
                            results.Add(new LogSearchResult
                            {
                                FilePath = filePath,
                                LineNumber = lineNumber,
                                LineContent = line
                            });
                        }
                    }
                }
            }

            return results;
        }

        private string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern)
                            .Replace(@"\*", ".*")
                            .Replace(@"\?", ".") + "$";
        }
    }
}
