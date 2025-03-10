using SGH.Data.Extensions;
using SGH.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Кастомные сервисы
builder.Services.AddPostgresDbContext(builder.Configuration);
builder.Services.AddJwtAuthentication();
builder.Services.AddMappers();
builder.Services.AddServices();
builder.Services.AddCommon();

var app = builder.Build();
await app.Services.ApplyMigrationAsync();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();