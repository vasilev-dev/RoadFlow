FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RoadFlow.Gateway.API/RoadFlow.Gateway.API.csproj", "RoadFlow.Gateway.API/"]
RUN dotnet restore "RoadFlow.Gateway.API/RoadFlow.Gateway.API.csproj"
COPY . .
WORKDIR "/src/RoadFlow.Gateway.API"
RUN dotnet build "RoadFlow.Gateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RoadFlow.Gateway.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoadFlow.Gateway.API.dll"]
