FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RoadFlow.Auth.API/RoadFlow.Auth.API.csproj", "RoadFlow.Auth.API/"]
RUN dotnet restore "RoadFlow.Auth.API/RoadFlow.Auth.API.csproj"
COPY . .
WORKDIR "/src/RoadFlow.Auth.API"
RUN dotnet build "RoadFlow.Auth.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RoadFlow.Auth.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoadFlow.Auth.API.dll"]
