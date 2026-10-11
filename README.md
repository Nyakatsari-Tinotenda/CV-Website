# Tinotenda Nyakatsari · Portfolio

My personal portfolio site: an **ASP.NET Core MVC** app on **.NET 10**, packaged with **Docker**, checked by **GitHub Actions** on every push and deployed to **Render**.

**Live site:** https://cv-website-ojn1.onrender.com

## How it ships

```
git push ──► GitHub Actions: docker build ─► run container ─► smoke test /healthz, /, CV download
        └──► Render: pulls the repo, builds the same Dockerfile, health-checks /healthz, goes live
```

- **Multi-stage Dockerfile.** The SDK image builds and publishes; only the slim ASP.NET runtime image ships, running as a non-root user.
- **Restore layer cached separately**, so code-only changes rebuild faster.
- **Health check** at `/healthz`, used by both CI and Render.
- **Proxy-aware.** Render terminates HTTPS, so the app reads `X-Forwarded-*` headers and listens on the `PORT` Render provides.
- **Hardened responses.** Content security policy, `nosniff`, frame blocking and a strict referrer policy on every response, plus Brotli/gzip compression and week-long caching for static files.
- **Live deployment info.** `/api/info` reports the deployed commit, branch, .NET version and container uptime, using the variables Render sets on each deploy. The homepage shows it in the "How this site ships" panel.
- **Infrastructure as code.** `render.yaml` describes the Render service, so it can be recreated from the repo.

## Run it locally

With Docker:

```bash
docker build -t tinotenda-portfolio .
docker run --rm -p 10000:10000 tinotenda-portfolio
# open http://localhost:10000
```

With the .NET 10 SDK:

```bash
dotnet run
# open http://localhost:8080
```

## Project layout

```
Controllers/HomeController.cs   Serves the portfolio page and the error page
Views/Home/Index.cshtml         The portfolio page
wwwroot/css, wwwroot/js         Styles and the transcript chart / copy-email script
wwwroot/img/                    Background and link-preview images
wwwroot/files/                  Downloadable CV (PDF)
Dockerfile, .dockerignore       Container build
render.yaml                     Render service definition
.github/workflows/ci.yml        Build + smoke-test pipeline
```
