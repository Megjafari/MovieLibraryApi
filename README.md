![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Render](https://img.shields.io/badge/Render-46E3B7?style=for-the-badge&logo=render&logoColor=white)

# MegFlix API 🎬

> RESTful API backend for **MegFlix** — a fullstack personal movie, series & anime library with JWT authentication, watchlists, and user reviews.

🌐 **Live Frontend:** [megflix.meghdadjafari.dev](https://megflix.meghdadjafari.dev)  
📦 **Frontend Repo:** [MegFlix Frontend](https://github.com/Megjafari/MegFlix)  
🚀 **API Base URL:** `https://movielibraryapi.onrender.com`

---

## Table of Contents

- [Overview](#overview)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [Authentication](#authentication)
- [API Reference](#api-reference)
  - [Auth](#auth)
  - [Movies](#movies)
  - [Reviews](#reviews)
- [Error Handling](#error-handling)
- [Deployment](#deployment)

---

## Overview

MegFlix API is a secure RESTful backend built with ASP.NET Core. It handles user authentication with JWT tokens, manages a personal movie/series/anime library per user, and supports full CRUD operations for movies and reviews.

Key features:
- JWT-based authentication with BCrypt password hashing
- User-specific movie lists and reviews
- Integration-ready for TMDB and Jikan APIs on the frontend
- PostgreSQL database hosted on Supabase
- Containerized with Docker and deployed on Render

---

## Tech Stack

| Technology | Purpose |
|------------|---------|
| ASP.NET Core | Web API framework |
| Entity Framework Core | ORM / database access |
| PostgreSQL (Supabase) | Database |
| JWT (JSON Web Tokens) | Authentication |
| BCrypt.Net | Password hashing |
| Docker | Containerization |
| Render | Cloud deployment |

---

## Architecture

```
MovieLibraryApi/
├── src/
│   ├── Controllers/
│   │   ├── AuthController.cs           # Register & login endpoints
│   │   ├── MoviesController.cs         # Movie CRUD
│   │   ├── ReviewsController.cs        # Review CRUD
│   │   └── WatchListController.cs      # Watchlist management
│   ├── Data/
│   │   └── AppDbContext.cs             # EF Core DbContext
│   ├── Dtos/
│   │   ├── MovieCreateDto.cs
│   │   ├── MovieResponseDto.cs
│   │   ├── MovieUpdateDto.cs
│   │   ├── NotInFutureAttribute.cs     # Custom validation attribute
│   │   ├── ReviewCreateDto.cs
│   │   ├── ReviewResponseDto.cs
│   │   └── ReviewUpdateDto.cs
│   ├── Models/
│   │   ├── Movie.cs
│   │   ├── Review.cs
│   │   ├── User.cs
│   │   └── Watchlist.cs
│   └── Services/
│       ├── IMovieService.cs            # Movie service interface
│       ├── IReviewService.cs           # Review service interface
│       ├── MovieService.cs             # Movie business logic
│       └── ReviewService.cs           # Review business logic
├── Migrations/                         # EF Core migrations
├── Dockerfile
├── Program.cs                          # App configuration & middleware
└── appsettings.json                    # Configuration
```

---

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/) or a [Supabase](https://supabase.com/) account
- [Docker](https://www.docker.com/) (optional)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Megjafari/MegFlixAPI.git
   cd MovieLibraryApi
   ```

2. **Create `appsettings.Development.json`**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "your-postgresql-connection-string"
     },
     "Jwt": {
       "Key": "your-super-secret-key-min-32-characters",
       "Issuer": "megflix-api",
       "Audience": "megflix-client"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

   The API will be available at `https://localhost:5098`

### Run with Docker

```bash
docker build -t megflix-api .
docker run -p 5098:5098 megflix-api
```

---

## Authentication

MegFlix API uses **JWT Bearer tokens** for authentication.

### How it works

1. Register or login to receive a JWT token
2. Include the token in the `Authorization` header for protected requests:
   ```
   Authorization: Bearer <your-token>
   ```
3. Tokens expire after **7 days**

### Password requirements
- Minimum 6 characters
- Stored as BCrypt hash — plain text passwords are never saved

---

## API Reference

### Base URL
```
https://movielibraryapi.onrender.com
```

---

### Auth

#### Register
```http
POST /api/auth/register
```

**Request body:**
```json
{
  "username": "johndoe",
  "email": "john@example.com",
  "password": "secret123"
}
```

**Validation:**
- Email must be a valid email address
- Password must be at least 6 characters
- Username must be at least 2 characters
- Email must be unique

**Response `200 OK`:**
```json
"User registered successfully"
```

**Response `400 Bad Request`:**
```json
"Email already exists"
```

---

#### Login
```http
POST /api/auth/login
```

**Request body:**
```json
{
  "email": "john@example.com",
  "password": "secret123"
}
```

**Response `200 OK`:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Response `401 Unauthorized`:**
```json
"Invalid credentials"
```

---

### Movies

> 🔒 All movie endpoints require authentication.

#### Get all movies
```http
GET /api/Movies
Authorization: Bearer <token>
```

**Response `200 OK`:**
```json
[
  {
    "id": 1,
    "title": "Inception",
    "releaseDate": "2010-07-16",
    "description": "A thief who steals corporate secrets..."
  }
]
```

---

#### Get movie by ID
```http
GET /api/Movies/{id}
Authorization: Bearer <token>
```

**Response `200 OK`:**
```json
{
  "id": 1,
  "title": "Inception",
  "releaseDate": "2010-07-16",
  "description": "A thief who steals corporate secrets..."
}
```

**Response `404 Not Found`**

---

#### Create movie
```http
POST /api/Movies
Authorization: Bearer <token>
Content-Type: application/json
```

**Request body:**
```json
{
  "title": "Inception",
  "releaseDate": "2010-07-16",
  "description": "A thief who steals corporate secrets..."
}
```

**Response `201 Created`**

---

#### Update movie
```http
PUT /api/Movies/{id}
Authorization: Bearer <token>
Content-Type: application/json
```

**Request body:**
```json
{
  "title": "Inception",
  "releaseDate": "2010-07-16",
  "description": "Updated description..."
}
```

**Response `204 No Content`**

---

#### Delete movie
```http
DELETE /api/Movies/{id}
Authorization: Bearer <token>
```

**Response `204 No Content`**

---

### Reviews

> 🔒 All review endpoints require authentication.

#### Get all reviews
```http
GET /api/Reviews
Authorization: Bearer <token>
```

**Response `200 OK`:**
```json
[
  {
    "id": 1,
    "movieId": 1,
    "comment": "Amazing film!",
    "rating": 5
  }
]
```

---

#### Create review
```http
POST /api/Reviews
Authorization: Bearer <token>
Content-Type: application/json
```

**Request body:**
```json
{
  "movieId": 1,
  "comment": "Amazing film!",
  "rating": 5
}
```

**Response `201 Created`**

---

#### Update review
```http
PUT /api/Reviews/{id}
Authorization: Bearer <token>
Content-Type: application/json
```

**Request body:**
```json
{
  "comment": "Still an amazing film!",
  "rating": 5
}
```

**Response `204 No Content`**

---

#### Delete review
```http
DELETE /api/Reviews/{id}
Authorization: Bearer <token>
```

**Response `204 No Content`**

---

## Error Handling

The API returns standard HTTP status codes:

| Status Code | Meaning |
|-------------|---------|
| `200 OK` | Request successful |
| `201 Created` | Resource created |
| `204 No Content` | Success, no body returned |
| `400 Bad Request` | Invalid input or validation error |
| `401 Unauthorized` | Missing or invalid JWT token |
| `404 Not Found` | Resource not found |
| `500 Internal Server Error` | Unexpected server error |

---

## Deployment

The API is deployed on **Render** using Docker.

> ⚠️ **Note:** This project uses Render's free tier. The API may take 1–2 minutes to respond after a period of inactivity as the server spins up from sleep mode.
|

---


### Environment variables on Render

| Variable | Description |
|----------|-------------|
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string (Supabase) |
| `Jwt__Key` | Secret key for JWT signing |
| `Jwt__Issuer` | JWT issuer (`megflix-api`) |
| `Jwt__Audience` | JWT audience (`megflix-client`) |


## Related

- 🎨 [MegFlix Frontend](https://megflix.meghdadjafari.dev)
- 💻 [Frontend Repository](https://github.com/Megjafari/MegFlix)
- 👩‍💻 [Portfolio](https://meghdadjafari.dev)
