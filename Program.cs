using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Services;
using DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPooledDbContextFactory<SSDBContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<LoginService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])
            ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Grupo", "Administradores"));
    options.AddPolicy("Usuarios", policy => policy.RequireClaim("Grupo", "Usuarios"));
});

builder.Services.AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<Consulta>()
    .AddMutationType(m => m.Name("Mutacion"))
        .AddType<ActividadProyectoService>()
        .AddType<ActividadRutaProyectoService>()
        .AddType<BitacoraProyectoService>()
        .AddType<ChatService>()
        .AddType<ComentarioService>()
        .AddType<MonedaService>()
        .AddType<PersonalService>()
        .AddType<ProyectoService>()
        .AddType<PublicacionService>()
        .AddType<RastreoPublicacionService>()
        .AddType<RecursoPublicacionService>()
        .AddType<SecuenciaService>()
        .AddType<TablaService>()
        .AddType<UsuarioService>()
        .AddType<VariosService>()
        .AddType<VersionApiService>()
    .AddProjections().AddFiltering().AddSorting();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    IDbContextFactory<SSDBContext> ctxFactory = scope.ServiceProvider
        .GetRequiredService<IDbContextFactory<SSDBContext>>();

    using (SSDBContext ctx = ctxFactory.CreateDbContext())
    {
        ctx.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGraphQL("/gql");

app.Run();
