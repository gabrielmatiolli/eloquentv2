# Use a imagem oficial do SDK do .NET para buildar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "EloquentBackend.csproj"
RUN dotnet publish "EloquentBackend.csproj" -c Release -o /app/publish

# Use a imagem oficial do ASP.NET Core para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EloquentBackend.dll"]