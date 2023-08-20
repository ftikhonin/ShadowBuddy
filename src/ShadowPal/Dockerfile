FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShadowPal/ShadowPal.csproj", "ShadowPal/"]
RUN dotnet restore "ShadowPal/ShadowPal.csproj"
COPY . .
WORKDIR "/src/ShadowPal"
RUN dotnet build "ShadowPal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShadowPal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShadowPal.dll"]
