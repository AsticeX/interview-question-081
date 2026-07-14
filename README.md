# IT Post Comments

Post and comment web app with two main parts:

- `src/server` - .NET 8 Web API, Entity Framework Core, SQL Server
- `src/client` - Angular 20, ng-zorro-antd

## Requirements

Install these tools before running the project:

- Docker Desktop
- Node.js and npm
- .NET SDK 8, only needed if you want to run the API without Docker

## Run API And Database

The recommended local setup is to run the API and SQL Server with Docker Compose.

```powershell
cd src/server
docker compose up --build
```

Services:

- API: `http://localhost:5028`
- Swagger: `http://localhost:5028/swagger`
- Health check: `http://localhost:5028/health`
- SQL Server: `localhost,1433`

Local database settings:

- Database: `SocialDb`
- User: `sa`
- Password: `LocalDev12345`

The API applies EF Core migrations on startup because `ApplyMigrationsOnStartup=true`.
If the `Posts` table is empty, the API also seeds one default post.

## Run Client

Open another terminal and start Angular.

```powershell
cd src/client
npm install
npm start
```

Open the app at:

```text
http://localhost:4200
```

The client calls `http://localhost:5028` from
`src/client/src/environments/environment.development.ts`.

## API Endpoints

Posts:

- `GET /api/post` - Get all posts
- `GET /api/post/{id}` - Get one post by id
- `POST /api/post/create` - Create a post
- `DELETE /api/post/{id}` - Delete a post

Comments:

- `GET /api/comment?postId={postId}` - Get comments for a post
- `GET /api/comment/{id}` - Get one comment by id
- `POST /api/comment/create` - Create a comment
- `DELETE /api/comment/{id}` - Delete a comment

Create comment example:

```json
{
  "postId": 1,
  "commentBy": "Blend 285",
  "message": "Hello"
}
```

## Comment Refresh

The client loads comments when the page opens.
After that, it refreshes comments every 5 seconds with `GET /api/comment?postId=...`.
This lets the page show new comments from other users without a browser refresh.

After sending a comment, the client:

1. Calls `POST /api/comment/create`
2. Calls `GET /api/comment?postId=...`
3. Replaces the comment list with the latest API data

## Useful Commands

Build client:

```powershell
cd src/client
npm run build
```

Run API locally without Docker. SQL Server must already be available at `localhost,1433`.

```powershell
cd src/server/WebAPI
dotnet run
```

Stop Docker containers:

```powershell
cd src/server
docker compose down
```

Stop Docker containers and delete the local database volume:

```powershell
cd src/server
docker compose down -v
```
