namespace TinotendaCV.Models
{
    public class PortfolioViewModel
    {
        public PersonalInfo PersonalInfo { get; set; } = new();
        public List<Skill> Skills { get; set; } = new();
        public List<Education> Educations { get; set; } = new();
        public List<Project> Projects { get; set; } = new();
        public List<Certification> Certifications { get; set; } = new();
    }

    public class PersonalInfo
    {
        public string Name { get; set; } = "";
        public string Title { get; set; } = "";
        public string Location { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string StudentId { get; set; } = "";
        public string GitHub { get; set; } = "";
        public string Bio { get; set; } = "";
    }

    public class Skill
    {
        public string Name { get; set; } = "";
        public string Level { get; set; } = "";
        public string Category { get; set; } = "";
        public int Percentage { get; set; }
    }

    public class Education
    {
        public string Institution { get; set; } = "";
        public string Degree { get; set; } = "";
        public string Duration { get; set; } = "";
        public string Description { get; set; } = "";
        public List<string> Modules { get; set; } = new();
    }

    public class Project
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public List<string> Technologies { get; set; } = new();
        public string Icon { get; set; } = "";
        public string GitHubUrl { get; set; } = "";
    }

    public class Certification
    {
        public string Name { get; set; } = "";
        public string Institution { get; set; } = "";
        public string Year { get; set; } = "";
    }

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
