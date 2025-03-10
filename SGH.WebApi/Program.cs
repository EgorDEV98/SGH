using SGH.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Кастомные сервисы
builder.Services.AddPostgresDbContext(builder.Configuration);

var app = builder.Build();
await app.Services.ApplyMigrationAsync();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();