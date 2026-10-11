using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Render (and most container hosts) tell the app which port to listen on via PORT.
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllersWithViews();
builder.Services.AddHealthChecks();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

// Render terminates HTTPS at its proxy and forwards plain HTTP to the container.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();
var startedAt = DateTimeOffset.UtcNow;

app.UseForwardedHeaders();

// Security headers on every response
const string csp =
    "default-src 'self'; " +
    "script-src 'self'; " +
    "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
    "font-src 'self' https://fonts.gstatic.com; " +
    "img-src 'self' data:; " +
    "connect-src 'self'; " +
    "frame-ancestors 'none'; base-uri 'self'; form-action 'self'";

app.Use(async (context, next) =>
{
    var headers = context.Response.Headers;
    headers["Content-Security-Policy"] = csp;
    headers["X-Content-Type-Options"] = "nosniff";
    headers["X-Frame-Options"] = "DENY";
    headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=()";
    await next();
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseResponseCompression();

// Cache static files for a week. CSS and JS carry a version hash (asp-append-version),
// so a new deploy still reaches visitors straight away.
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
        ctx.Context.Response.Headers["Cache-Control"] = "public, max-age=604800"
});

app.UseRouting();

app.MapHealthChecks("/healthz");

// Live deployment details for the "How this site ships" panel.
// Render sets RENDER_GIT_COMMIT and RENDER_GIT_BRANCH on every deploy.
app.MapGet("/api/info", () => Results.Json(new
{
    commit = Environment.GetEnvironmentVariable("RENDER_GIT_COMMIT"),
    branch = Environment.GetEnvironmentVariable("RENDER_GIT_BRANCH"),
    service = Environment.GetEnvironmentVariable("RENDER_SERVICE_NAME"),
    environment = app.Environment.EnvironmentName,
    dotnet = Environment.Version.ToString(),
    startedAt,
    uptimeSeconds = (long)(DateTimeOffset.UtcNow - startedAt).TotalSeconds
}));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
