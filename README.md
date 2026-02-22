# 🎬 MovieLibraryApi

ASP.NET Core Web API skoluppgift – ett film- och recensionsbibliotek.

## Tekniker
- ASP.NET Core Web API
- Entity Framework Core + SQL Server
- DTOs + Services + Dependency Injection
- Swagger UI

## Frontend
Frontend till detta API finns här:
[MovieLibraryFrontend](https://github.com/Megjafari/MovieLibraryFrontend)

## Kom igång

### Krav
- .NET 8 SDK
- SQL Server

### Starta projektet
```bash
dotnet ef database update
dotnet run
```

Swagger UI: `https://localhost:7056/swagger`

## Endpoints

### Movies
| Method | Endpoint | Beskrivning |
|--------|----------|-------------|
| GET | /api/Movies | Hämta alla filmer |
| GET | /api/Movies/{id} | Hämta en film |
| POST | /api/Movies | Skapa film |
| PUT | /api/Movies/{id} | Uppdatera film |
| DELETE | /api/Movies/{id} | Ta bort film |

### Reviews
| Method | Endpoint | Beskrivning |
|--------|----------|-------------|
| GET | /api/Reviews | Hämta alla recensioner |
| GET | /api/Reviews/{id} | Hämta en recension |
| POST | /api/Reviews | Skapa recension |
| PUT | /api/Reviews/{id} | Uppdatera recension |
| DELETE | /api/Reviews/{id} | Ta bort recension |

## Datamodell

**Movie**
- Id, Title, Description, ReleaseDate

**Review**
- Id, Comment, Rating (1-5), MovieId


## Screenshots

### Swagger UI
<img width="800" alt="Swagger UI" src="https://github.com/user-attachments/assets/8cbf8c0e-5cf0-4922-b9fd-80531c67410f" />
