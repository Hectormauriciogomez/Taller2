using Microsoft.EntityFrameworkCore;
using TMecanicoC.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Sentencias para configurar la cadena de conexión con la base de datos
builder.Services.AddDbContext<DBTMecanicoCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

// Sentencias incluidas para estructurar el munú dinámico de este proyecto
builder.Services.AddDbContext < DBTMecanicoCContext>();
// Habilitar la sesión para almacenar información y compartirla dentro de este proyecto de forma general
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(opciones =>
{
    opciones.IdleTimeout = TimeSpan.FromMinutes(10);
    opciones.Cookie.HttpOnly = true;
    opciones.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configurar la fuente de información para la solicitud HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es de 30 días.
    // Es posible que desee cambiar esto para escenarios de producción, consulte https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

// Aquí es donde inicia la aplicación (Controlador y vista)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Personas}/{action=Create}");
app.Run();
