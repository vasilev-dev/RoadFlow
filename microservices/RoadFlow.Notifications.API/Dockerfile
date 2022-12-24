FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RoadFlow.Notifications.API/RoadFlow.Notifications.API.csproj", "RoadFlow.Notifications.API/"]
RUN dotnet restore "RoadFlow.Notifications.API/RoadFlow.Notifications.API.csproj"
COPY . .
WORKDIR "/src/RoadFlow.Notifications.API"
RUN dotnet build "RoadFlow.Notifications.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RoadFlow.Notifications.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoadFlow.Notifications.API.dll"]
