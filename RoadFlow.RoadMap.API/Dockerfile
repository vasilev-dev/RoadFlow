FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RoadFlow.RoadMap.API/RoadFlow.RoadMap.API.csproj", "RoadFlow.RoadMap.API/"]
RUN dotnet restore "RoadFlow.RoadMap.API/RoadFlow.RoadMap.API.csproj"
COPY . .
WORKDIR "/src/RoadFlow.RoadMap.API"
RUN dotnet build "RoadFlow.RoadMap.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RoadFlow.RoadMap.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoadFlow.RoadMap.API.dll"]
