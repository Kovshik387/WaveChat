#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Systems/Identity/WaveChat.IdentityServer/WaveChat.IdentityServer.csproj", "src/Systems/Identity/WaveChat.IdentityServer/"]
COPY ["src/Data/Contex/WaveChat.Context/WaveChat.Context.csproj", "src/Data/Contex/WaveChat.Context/"]
COPY ["src/Shared/WaveChat.Common.csproj", "src/Shared/"]
COPY ["src/Data/WaveChat.Context.Entities/WaveChat.Context.Entities.csproj", "src/Data/WaveChat.Context.Entities/"]
COPY ["src/Services/WaveChat.Services.Settings/WaveChat.Services.Settings.csproj", "src/Services/WaveChat.Services.Settings/"]
RUN dotnet restore "./src/Systems/Identity/WaveChat.IdentityServer/WaveChat.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/Systems/Identity/WaveChat.IdentityServer"
RUN dotnet build "./WaveChat.IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WaveChat.IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WaveChat.IdentityServer.dll"]