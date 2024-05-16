#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Systems/Api/WaveChat.Storage/WaveChat.Storage.csproj", "src/Systems/Api/WaveChat.Storage/"]
COPY ["src/Data/Contex/WaveChat.Context/WaveChat.Context.csproj", "src/Data/Contex/WaveChat.Context/"]
COPY ["src/Shared/WaveChat.Common.csproj", "src/Shared/"]
COPY ["src/Data/WaveChat.Context.Entities/WaveChat.Context.Entities.csproj", "src/Data/WaveChat.Context.Entities/"]
COPY ["src/Data/Migrations/WaveChat.Context.Migrations.PgSql/WaveChat.Context.Migrations.PgSql.csproj", "src/Data/Migrations/WaveChat.Context.Migrations.PgSql/"]
COPY ["src/Data/WaveChat.Context.Seeder/WaveChat.Context.Seeder.csproj", "src/Data/WaveChat.Context.Seeder/"]
COPY ["src/Services/WaveChat.Services.Authorization/WaveChat.Services.Authorization.csproj", "src/Services/WaveChat.Services.Authorization/"]
COPY ["src/Services/WaveChat.Services.Settings/WaveChat.Services.Settings.csproj", "src/Services/WaveChat.Services.Settings/"]
COPY ["src/Services/WaveChat.Services.Logger/WaveChat.Services.Logger.csproj", "src/Services/WaveChat.Services.Logger/"]
RUN dotnet restore "./src/Systems/Api/WaveChat.Storage/WaveChat.Storage.csproj"
COPY . .
WORKDIR "/src/src/Systems/Api/WaveChat.Storage"
RUN dotnet build "./WaveChat.Storage.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WaveChat.Storage.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WaveChat.Storage.dll"]