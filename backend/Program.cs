using EloquentBackend.Data;
using EloquentBackend.Interfaces.Services;
using EloquentBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPerkService, PerkService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ISubscriptionPerkService, SubscriptionPerkService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

// =================================================================
// INÍCIO: Seção para aplicar migrações em produção
// =================================================================

// Cria um "escopo" de serviço para obter o DbContext
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Pega o seu DbContext (substitua ApiDbContext pelo nome do seu)
        var context = services.GetRequiredService<ApiDbContext>();

        // Pega o logger para registrar informações
        var logger = services.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("Verificando se existem migrações pendentes...");

        // Aplica quaisquer migrações pendentes no banco de dados.
        // Isso irá criar o banco de dados se ele ainda não existir.
        context.Database.Migrate();

        logger.LogInformation(
            "Migrações aplicadas com sucesso ou banco de dados já está atualizado."
        );
    }
    catch (Exception ex)
    {
        // Se algo der errado, registra o erro. Isso é crucial para depuração.
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrações do banco de dados.");

        // Opcional: Impedir que a aplicação inicie se as migrações falharem.
        // throw;
    }
}

// =================================================================
// FIM: Seção para aplicar migrações
// =================================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
