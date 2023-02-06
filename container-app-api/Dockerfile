#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["container-app-api/container-app-api.csproj", "container-app-api/"]
RUN dotnet restore "container-app-api/container-app-api.csproj"
COPY . .
WORKDIR "/src/container-app-api"
RUN dotnet build "container-app-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "container-app-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "container-app-api.dll"]