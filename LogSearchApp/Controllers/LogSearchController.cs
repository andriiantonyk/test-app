using LogSearchConsole.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace LogSearchApp.Controllers
{
    public class LogSearchController : Controller
    {
        private readonly ILogSearchService _logSearchService;

        public LogSearchController(ILogSearchService logSearchService)
        {
            _logSearchService = logSearchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                ViewBag.Error = "Search pattern cannot be empty.";
                return View("Index");
            }

            var results = _logSearchService.SearchLogs(pattern);
            ViewBag.Results = results;
            return View("Index");
        }

        public IActionResult ViewLogFile(string filePath, int lineNumber)
        {
            var lines = System.IO.File.ReadAllLines(filePath).ToList();
            ViewBag.Lines = lines;
            ViewBag.LineNumber = lineNumber;
            ViewBag.FilePath = filePath;
            return View();
        }
    }
}
