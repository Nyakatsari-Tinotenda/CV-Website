using Microsoft.AspNetCore.Mvc;
using TinotendaCV.Models;
using System.Diagnostics;

namespace TinotendaCV.Controllers
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
            var viewModel = new PortfolioViewModel
            {
                PersonalInfo = new PersonalInfo
                {
                    Name = "Tinotenda Nyakatsari",
                    Title = "Software Developer",
                    Location = "South Africa",
                    Email = "tinotendanyakatsarii2@gmail.com",
                    Phone = "074 459 6617",
                    StudentId = "ST10437488",
                    GitHub = "Nyakatsari-Tinotenda",
                    Bio = "Passionate software developer specializing in C#, Cloud Development, and Cybersecurity. Currently pursuing a Bachelor's degree in Computer and Information Sciences with a focus on building innovative solutions."
                },
                Skills = new List<Skill>
                {
                    new Skill { Name = "C# Programming", Level = "Exceptional", Category = "Programming", Percentage = 95 },
                    new Skill { Name = "Cloud Development", Level = "Exceptional", Category = "Cloud", Percentage = 92 },
                    new Skill { Name = "Network Engineering", Level = "Exceptional", Category = "Systems", Percentage = 90 },
                    new Skill { Name = "Cybersecurity", Level = "Exceptional", Category = "Security", Percentage = 88 },
                    new Skill { Name = "Information Systems", Level = "Exceptional", Category = "Systems", Percentage = 91 },
                    new Skill { Name = "Programming Logic", Level = "Exceptional", Category = "Programming", Percentage = 93 },
                    new Skill { Name = "Computer Systems", Level = "Exceptional", Category = "Systems", Percentage = 89 },
                    new Skill { Name = "Web Development", Level = "Exceptional", Category = "Programming", Percentage = 87 },
                    new Skill { Name = "Security Principles", Level = "Exceptional", Category = "Security", Percentage = 90 }
                },
                Educations = new List<Education>
                {
                    new Education
                    {
                        Institution = "The Independent Institute of Education",
                        Degree = "Bachelor of Computer and Information Sciences",
                        Duration = "2024 - Present",
                        Description = "Currently pursuing a Bachelor's degree in Application Development (NQF Level 7). Focusing on advanced programming, cloud development, and information systems.",
                        Modules = new List<string>
                        {
                            "Programming 1A & 1B - Passed with Distinction",
                            "Cloud Development A & B - Passed with Distinction",
                            "Network Engineering 1A & 1B - Passed with Distinction",
                            "Information Systems - Passed with Distinction",
                            "Principles of Security - Passed"
                        }
                    },
                    new Education
                    {
                        Institution = "Bernard Mizeki College",
                        Degree = "Advanced Level (A-Levels)",
                        Duration = "2020 - 2022",
                        Description = "Completed Cambridge International A-Levels with focus on Computer Science and Business studies.",
                        Modules = new List<string>
                        {
                            "Computer Science - Grade C",
                            "Business - Grade C",
                            "English General Paper - Grade B",
                            "Accounting - Grade C"
                        }
                    },
                    new Education
                    {
                        Institution = "Bernard Mizeki College / Ministry of Higher Education",
                        Degree = "IGCSE & National Foundation Certificate",
                        Duration = "2019 - 2020",
                        Description = "Achieved IGCSE certification and National Foundation Certificate in Computer Systems Installation, Servicing and Repairs.",
                        Modules = new List<string>
                        {
                            "Computer Science - Grade B",
                            "History - Grade A",
                            "First Language English - Grade B",
                            "National Foundation Certificate - Computer Systems"
                        }
                    }
                },
                Projects = new List<Project>
                {
                    new Project
                    {
                        Name = "Cybersecurity Chatbot",
                        Description = "An interactive C# console application that provides cybersecurity awareness and guidance. Features include user authentication, security tips, threat information, and interactive responses.",
                        Technologies = new List<string> { "C#", ".NET", "Console App", "Cybersecurity" },
                        Icon = "shield-lock",
                        GitHubUrl = "https://github.com/Nyakatsari-Tinotenda/cybersecurity-chatbot-ST10437488"
                    },
                    new Project
                    {
                        Name = "ABC Retail Application",
                        Description = "A comprehensive retail management system built with C#, HTML, CSS, and JavaScript. Features product management, order processing, and customer management functionality.",
                        Technologies = new List<string> { "C#", "HTML5", "CSS3", "JavaScript" },
                        Icon = "shop",
                        GitHubUrl = "https://github.com/Nyakatsari-Tinotenda/CLDV-finalPOE"
                    }
                },
                Certifications = new List<Certification>
                {
                    new Certification { Name = "National Foundation Certificate", Institution = "Ministry of Higher Education, Zimbabwe", Year = "2019" }
                }
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
