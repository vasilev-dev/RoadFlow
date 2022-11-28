FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RoadFlow.Showcase.API/RoadFlow.Showcase.API.csproj", "RoadFlow.Showcase.API/"]
RUN dotnet restore "RoadFlow.Showcase.API/RoadFlow.Showcase.API.csproj"
COPY . .
WORKDIR "/src/RoadFlow.Showcase.API"
RUN dotnet build "RoadFlow.Showcase.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RoadFlow.Showcase.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoadFlow.Showcase.API.dll"]
