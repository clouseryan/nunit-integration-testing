FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pokemon.Web.API/Pokemon.Web.API.csproj", "Pokemon.Web.API/"]
RUN dotnet restore "Pokemon.Web.API/Pokemon.Web.API.csproj"
COPY . .
WORKDIR "/src/Pokemon.Web.API"
RUN dotnet build "Pokemon.Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokemon.Web.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokemon.Web.API.dll"]
