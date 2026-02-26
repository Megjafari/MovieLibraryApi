![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Render](https://img.shields.io/badge/Render-46E3B7?style=for-the-badge&logo=render&logoColor=white)


# MegFlix API 🎬

RESTful API backend for MegFlix, a movie review application.

**Frontend:** https://megflix.vercel.app/

## Tech Stack

- ASP.NET Core 10 Web API
- Entity Framework Core
- PostgreSQL (Supabase)
- Docker
- Deployed on Render

## Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/Movies | Get all movies |
| GET | /api/Movies/{id} | Get movie by id |
| POST | /api/Movies | Create movie |
| PUT | /api/Movies/{id} | Update movie |
| DELETE | /api/Movies/{id} | Delete movie |
| GET | /api/Reviews | Get all reviews |
| POST | /api/Reviews | Create review |
| PUT | /api/Reviews/{id} | Update review |
| DELETE | /api/Reviews/{id} | Delete review |

## Getting Started

1. Clone the repo
2. Create `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your-postgresql-connection-string"
  }
}
```
3. Run `dotnet ef database update`
4. Run `dotnet run`

## Related

- [MegFlix Frontend](https://github.com/Megjafari/MovieLibraryFrontend)
