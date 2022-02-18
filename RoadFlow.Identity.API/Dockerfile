FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RoadFlow.Identity.API/RoadFlow.Identity.API.csproj", "RoadFlow.Identity.API/"]
RUN dotnet restore "RoadFlow.Identity.API/RoadFlow.Identity.API.csproj"
COPY . .
WORKDIR "/src/RoadFlow.Identity.API"
RUN dotnet build "RoadFlow.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RoadFlow.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoadFlow.Identity.API.dll"]
