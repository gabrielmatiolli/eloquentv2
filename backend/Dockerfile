# Estágio 'build' (para produção, continua o mesmo)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out

# NOVO Estágio 'development'
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS development
WORKDIR /app

RUN dotnet tool install --global dotnet-ef

ENV PATH "$PATH:/root/.dotnet/tools"
# Apenas restauramos as dependências. O código será montado via volume.
COPY *.csproj .
RUN dotnet restore
# O comando de execução (dotnet watch) será fornecido pelo docker-compose.override.yml

# Estágio 'runtime' (para produção, continua o mesmo)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "EloquentBackend.dll"]