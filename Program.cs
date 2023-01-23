using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Entidades;
using Services;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPooledDbContextFactory<SSDBContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) //.LogTo(Console.WriteLine)
);

builder.Services.AddHttpContextAccessor();
// builder.Services.AddSingleton<IUriService>(o =>
//     {
//         var accessor = o.GetRequiredService<IHttpContextAccessor>();
//         var request = accessor!.HttpContext!.Request;
//         var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
//         return new UriService(uri);
//     });

// builder.Services.AddScoped<ChatService>();
// builder.Services.AddScoped<CiudadService>();
// builder.Services.AddScoped<ComentarioService>();
// builder.Services.AddScoped<EstadoService>();
// builder.Services.AddScoped<EstatusPublicacionService>();
// builder.Services.AddScoped<GrupoService>();
// builder.Services.AddScoped<PaisService>();
// builder.Services.AddScoped<PersonalService>();
// builder.Services.AddScoped<ProfesionService>();
// builder.Services.AddScoped<PublicacionService>();
// builder.Services.AddScoped<RolService>();
// builder.Services.AddScoped<TipoPublicacionService>();
// builder.Services.AddScoped<UsuarioService>();

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
    options.AddPolicy("Admin", policy => policy.RequireClaim("Grupo", "Administradores"))
);

builder.Services.AddGraphQLServer()
    .AddQueryType<Consulta>()
    .AddMutationType(m => m.Name("Mutacion"))
        .AddType<ChatService>()
        .AddType<CiudadService>()
        .AddType<ComentarioService>()
        .AddType<EstadoService>()
        .AddType<EstatusPublicacionService>()
        .AddType<GrupoService>()
        .AddType<PaisService>()
        .AddType<PersonalService>()
        .AddType<ProfesionService>()
        .AddType<PublicacionService>()
        .AddType<RolService>()
        .AddType<TipoPublicacionService>()
        .AddType<UsuarioService>()
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
// app.UseEndpoints(ep =>
// {
//     ep.MapControllers();
//     ep.MapGraphQL("/gql").RequireAuthorization();
// });

app.Run();
