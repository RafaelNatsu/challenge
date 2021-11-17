FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["getSiteFile.csproj", "./"]
RUN dotnet restore "getSiteFile.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "getSiteFile.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "getSiteFile.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "getSiteFile.dll"]
