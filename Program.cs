using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Services;
using DB;
using FluentValidation.AspNetCore;
using Validadores;

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

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddTransient<ValidadorActividadProyectoRequeridos>();
builder.Services.AddTransient<ValidadorActividadRutaProyectoRequeridos>();
builder.Services.AddTransient<ValidadorBitacoraProyectoRequeridos>();
builder.Services.AddTransient<ValidadorChatRequeridos>();
builder.Services.AddTransient<ValidadorComentarioRequeridos>();
builder.Services.AddTransient<ValidadorMonedaRequeridos>();
builder.Services.AddTransient<ValidadorPersonalRequeridos>();
builder.Services.AddTransient<ValidadorProyectoRequeridos>();
builder.Services.AddTransient<ValidadorPublicacionRequeridos>();
builder.Services.AddTransient<ValidadorRastreoPublicacionRequeridos>();
builder.Services.AddTransient<ValidadorRecursoPublicacionRequeridos>();
builder.Services.AddTransient<ValidadorSecuenciaRequeridos>();
builder.Services.AddTransient<ValidadorTablaRequeridos>();
builder.Services.AddTransient<ValidadorUsuarioRequeridos>();
builder.Services.AddTransient<ValidadorVariosRequeridos>();
builder.Services.AddTransient<ValidadorVersionApiRequeridos>();
builder.Services.AddTransient<ValidadorActividadProyecto>();
builder.Services.AddTransient<ValidadorActividadRutaProyecto>();
builder.Services.AddTransient<ValidadorBitacoraProyecto>();
builder.Services.AddTransient<ValidadorChat>();
builder.Services.AddTransient<ValidadorComentario>();
builder.Services.AddTransient<ValidadorMoneda>();
builder.Services.AddTransient<ValidadorPersonal>();
builder.Services.AddTransient<ValidadorProyecto>();
builder.Services.AddTransient<ValidadorPublicacion>();
builder.Services.AddTransient<ValidadorRastreoPublicacion>();
builder.Services.AddTransient<ValidadorRecursoPublicacion>();
builder.Services.AddTransient<ValidadorSecuencia>();
builder.Services.AddTransient<ValidadorTabla>();
builder.Services.AddTransient<ValidadorUsuario>();
builder.Services.AddTransient<ValidadorVarios>();
builder.Services.AddTransient<ValidadorVersionApi>();

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
