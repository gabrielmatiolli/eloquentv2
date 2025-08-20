# backend/Dockerfile (Arquivo Corrigido)

# --- ESTÁGIO 1: BUILD (Produção) ---
# Usa a imagem do SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# **CORREÇÃO**: Copia o arquivo .csproj de sua subpasta específica primeiro.
# !! IMPORTANTE !! Ajuste "EloquentBackend/EloquentBackend.csproj" se o nome da pasta ou do arquivo for diferente.
COPY ["EloquentBackend/EloquentBackend.csproj", "EloquentBackend/"]

# Restaura as dependências do projeto específico
RUN dotnet restore "EloquentBackend/EloquentBackend.csproj"

# Copia todo o resto do código-fonte para o diretório /src
COPY . .

# Entra na pasta do projeto e publica a aplicação
WORKDIR "/src/EloquentBackend"
RUN dotnet publish "EloquentBackend.csproj" -c Release -o /app/publish


# --- ESTÁGIO 2: DEVELOPMENT ---
# Usa a imagem do SDK para o ambiente de desenvolvimento
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS development
WORKDIR /app

# Instala as ferramentas do Entity Framework
RUN dotnet tool install --global dotnet-ef
ENV PATH "$PATH:/root/.dotnet/tools"

# **CORREÇÃO**: Copia o arquivo .csproj para restaurar as dependências na imagem base.
# O código-fonte será montado por cima via volume, mas as dependências precisam estar aqui.
# !! IMPORTANTE !! Ajuste "EloquentBackend.csproj" se o nome do arquivo for diferente.
COPY ["backend/EloquentBackend/EloquentBackend.csproj", "."]
RUN dotnet restore "EloquentBackend.csproj"

# O comando para iniciar (dotnet watch) será definido no docker-compose.override.yml


# --- ESTÁGIO 3: RUNTIME (Produção) ---
# Usa a imagem de runtime do ASP.NET, que é menor e mais segura
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia a aplicação publicada do estágio 'build'
COPY --from=build /app/publish .

# Expõe a porta que a aplicação irá usar
EXPOSE 8080

# Define o comando para iniciar a aplicação
# !! IMPORTANTE !! Verifique se "EloquentBackend.dll" é o nome correto do seu assembly.
ENTRYPOINT ["dotnet", "EloquentBackend.dll"]
```
# backend/.dockerignore (Novo Arquivo)

# Ignora pastas de build do .NET. Elas serão geradas dentro do container.
**/bin
**/obj

# Ignora pastas de configuração de IDEs
.vs/
.vscode/

# Ignora arquivos de usuário do Visual Studio
*.suo
*.user
