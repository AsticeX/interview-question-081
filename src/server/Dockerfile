FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY test.sln ./
COPY Domain/Domain.csproj Domain/
COPY Application/Application.csproj Application/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY WebAPI/WebAPI.csproj WebAPI/

RUN dotnet restore WebAPI/WebAPI.csproj

COPY . .
RUN dotnet publish WebAPI/WebAPI.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "WebAPI.dll"]
