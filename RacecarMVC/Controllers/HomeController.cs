using Microsoft.AspNetCore.Mvc;
using RacecarMVC.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RacecarMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reverse()
        {
            Palindrome model = new();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reverse(Palindrome palindrome)
        {
            string inputWord = palindrome.InputWord;
            string revWord = string.Empty;

            for(int i = inputWord.Length - 1; i >= 0; i--)
            {
                revWord += inputWord[i];
            }

            palindrome.RevWord = revWord;

            string pattern = @"[\W_]"; // Unicode Aware
            //string pattern = @"[^a-zA-Z0-9]"; // ASCII only

            revWord = Regex.Replace(revWord.ToLower(), pattern, "");
            inputWord = Regex.Replace(inputWord.ToLower(), pattern, "");

            if (revWord == inputWord)
            {
                palindrome.IsPalindrome = true;
                palindrome.Message = $"Success {palindrome.InputWord} is a Palindrome";
            }
            else
            {
                palindrome.IsPalindrome = false;
                palindrome.Message = $"Success {palindrome.InputWord} is NOT a Palindrome";
            }

            return View(palindrome);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}