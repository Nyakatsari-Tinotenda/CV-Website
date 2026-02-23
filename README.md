# Tinotenda Nyakatsari - CV Website

A professional portfolio website built with **ASP.NET Core MVC** on **.NET 10.0**.

## Features

- **Modern Dark Theme** with baby blue accents
- **Fully Responsive** design for all devices
- **Smooth Animations** with AOS library
- **Skill Filtering** by category
- **Animated Skill Bars**
- **Interactive Timeline** for education
- **Project Showcase** with GitHub links
- **Contact Form** with validation

## Technologies

- ASP.NET Core MVC 10.0
- C# 13
- Razor Views
- Bootstrap 5.3
- JavaScript (ES6+)
- AOS Animation Library

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (Preview)
- Visual Studio 2022 17.13+ or VS Code

## Running the Application

### Option 1: Using .NET CLI

```bash
# Navigate to project folder
cd TinotendaCV_MVC

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

Then open your browser to:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:7000`

### Option 2: Using Visual Studio

1. Open `TinotendaCV.sln` in Visual Studio 2022
2. Press `F5` or click the "Run" button

### Option 3: Using VS Code

1. Open the project folder in VS Code
2. Install the C# Dev Kit extension
3. Press `F5` to run

## Project Structure

```
TinotendaCV_MVC/
├── Controllers/
│   └── HomeController.cs       # Main controller
├── Models/
│   └── PortfolioViewModel.cs   # View models
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml        # Main portfolio page
│   │   └── Privacy.cshtml      # Privacy policy
│   └── Shared/
│       ├── _Layout.cshtml      # Main layout
│       └── Error.cshtml        # Error page
├── wwwroot/
│   ├── css/
│   │   └── site.css            # Custom styles
│   ├── js/
│   │   └── site.js             # Interactive JavaScript
│   └── images/                 # Images folder
├── Properties/
│   └── launchSettings.json     # Launch configuration
├── TinotendaCV.csproj          # Project file
├── Program.cs                  # Application entry point
└── README.md                   # This file
```

## Customization

### Update Personal Information

Edit `Controllers/HomeController.cs` to update:
- Personal details (name, email, phone, etc.)
- Skills
- Education history
- Projects

### Change Colors

Edit `wwwroot/css/site.css` and modify the CSS variables:

```css
:root {
    --primary-color: #87CEEB;      /* Baby blue */
    --primary-light: #B0E0E6;
    --primary-dark: #5F9EA0;
    --dark-bg: #0a0a0a;            /* Dark background */
    --card-bg: #141414;            /* Card background */
}
```

## Building for Production

```bash
dotnet publish -c Release -o ./publish
```

The published files will be in the `publish` folder.

## Deployment

### Azure App Service

```bash
az webapp up --name tinotenda-cv --runtime "DOTNET:10.0"
```

### IIS

1. Publish the application
2. Copy files to IIS directory
3. Configure application pool for .NET 10.0

## License

This project is for personal use.

## Contact

- **Email:** tinotendanyakatsarii2@gmail.com
- **Phone:** 074 459 6617
- **GitHub:** [Nyakatsari-Tinotenda](https://github.com/Nyakatsari-Tinotenda)
