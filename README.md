# IT Post Comments

A simple post and comment web application built with:

* `src/server` — .NET 8 Web API, Entity Framework Core, SQL Server
* `src/client` — Angular 20, ng-zorro-antd

## Prerequisites

Install the following tools before running the project:

* Docker Desktop
* Node.js and npm
* .NET SDK 8, only required when running the API without Docker

> SQL Server does not need to be installed locally when using Docker Compose.

## Run API and Database

The recommended setup is to run the API and SQL Server with Docker Compose.

```powershell
cd src/server
docker compose up --build
```

On the first startup, SQL Server may take a moment to become ready. The API will start after the database is available.

Available services:

* API: `http://localhost:5028`
* Swagger: `http://localhost:5028/swagger`
* Health check: `http://localhost:5028/health`
* SQL Server: `localhost,1433`

### Local Database Settings

* Database: `SocialDb`
* User: `sa`
* Password: `LocalDev12345`

> These credentials are for local development and assessment purposes only. They must not be used in production.

The API applies EF Core migrations automatically on startup because `ApplyMigrationsOnStartup=true`.

If the `Posts` table is empty, the API seeds one default post so the application does not start with an empty page.

## Run Client

Open another terminal and run:

```powershell
cd src/client
npm install
npm start
```

Open the application at:

```text
http://localhost:4200
```

The development client connects to:

```text
http://localhost:5028
```

The API URL is configured in:

```text
src/client/src/environments/environment.development.ts
```

## Verify the Application

After starting Docker Compose:

1. Open `http://localhost:5028/health`
2. Open `http://localhost:5028/swagger`
3. Start the Angular client
4. Open `http://localhost:4200`
5. Add a comment and verify that it appears in the comment list

## API Endpoints

### Posts

* `GET /api/post` — Get all posts
* `GET /api/post/{id}` — Get a post by ID
* `POST /api/post` — Create a post
* `DELETE /api/post/{id}` — Delete a post

### Comments

* `GET /api/comment?postId={postId}` — Get comments for a post
* `GET /api/comment/{id}` — Get a comment by ID
* `POST /api/comment` — Create a comment
* `DELETE /api/comment/{id}` — Delete a comment

### Create Comment Example

```json
{
  "postId": 1,
  "commentBy": "Blend 285",
  "message": "Hello"
}
```

## Comment Refresh

The client loads comments when the page opens.

After the initial load, it requests the latest comments every 5 seconds using:

```text
GET /api/comment?postId={postId}
```

This allows comments submitted by other users to appear without manually refreshing the browser.

After submitting a comment, the client:

1. Calls `POST /api/comment`
2. Calls `GET /api/comment?postId={postId}`
3. Replaces the current comment list with the latest data returned by the API

## Useful Commands

### Build Client

```powershell
cd src/client
npm run build
```

### Run API Without Docker

SQL Server must already be available at `localhost,1433`.

```powershell
cd src/server/WebAPI
dotnet run
```

### Stop Docker Containers

```powershell
cd src/server
docker compose down
```

### Stop Containers and Delete the Local Database Volume

```powershell
cd src/server
docker compose down -v
```
