using Entidades;
using Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SSDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<CiudadService>();
builder.Services.AddScoped<ComentarioService>();
builder.Services.AddScoped<EstadoService>();
builder.Services.AddScoped<EstatusPublicacionService>();
builder.Services.AddScoped<PaisService>();
builder.Services.AddScoped<PersonalService>();
builder.Services.AddScoped<ProfesionService>();
builder.Services.AddScoped<PublicacionService>();
builder.Services.AddScoped<RolService>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var ctx = scope.ServiceProvider.GetRequiredService<SSDBContext>();
//     ctx.Database.Migrate();
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();