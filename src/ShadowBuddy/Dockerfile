FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShadowBuddy/ShadowBuddy.csproj", "ShadowBuddy/"]
RUN dotnet restore "ShadowBuddy/ShadowBuddy.csproj"
COPY . .
WORKDIR "/src/ShadowBuddy"
RUN dotnet build "ShadowBuddy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShadowBuddy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShadowBuddy.dll"]
