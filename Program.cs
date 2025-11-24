using Microsoft.EntityFrameworkCore;
using Sucursal.Core.Interfaces;
using Sucursal.Infraestructura.Clientes;
using Sucursal.Infraestructura.Data;
using Sucursal.Infraestructura.Repositorios;
using Sucursal.Infraestructura.Servicios;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURACIÓN DEL PUERTO 

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Cargar appsettings
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// 2. BASE DE DATOS (SEGÚN TUS NOTAS)

var urlRailway = Environment.GetEnvironmentVariable("DATABASE_PUBLIC_URL");

var connectionString = urlRailway ?? builder.Configuration.GetConnectionString("SucursalContext");

// Imprimir en consola para depurar (como dicen tus notas)
if (!string.IsNullOrEmpty(urlRailway))
{
    Console.WriteLine($"--> USANDO BASE DE DATOS RAILWAY: {urlRailway}");
}
else
{
    Console.WriteLine("--> USANDO BASE DE DATOS LOCAL");
}

builder.Services.AddDbContext<SucursalDbContext>(options =>
    options.UseNpgsql(connectionString));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Repositorios
builder.Services.AddScoped<ISolicitudRepository, SolicitudRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();

// ==================== REGISTRAR TODOS LOS 17 HttpClients ====================

// 1. VENTAS - Reportes de ventas, transacciones
builder.Services.AddHttpClient<IVentasClient, VentasClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Ventas"]
        ?? "http://localhost:5001");
});

// 2. ALMACÉN SUCURSAL - Inventario local, stock
builder.Services.AddHttpClient<IAlmacenClient, AlmacenClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Almacen"]
        ?? "http://localhost:5002");
});

// 3. RRHH - Empleados, asistencia, nómina
builder.Services.AddHttpClient<IRRHHClient, RRHHClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:RRHH"]
        ?? "http://localhost:5003");
});

// 4. CONTABILIDAD - Presupuestos, estados financieros
builder.Services.AddHttpClient<IContabilidadClient, ContabilidadClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Contabilidad"]
        ?? "http://localhost:5004");
});

// 5. MARKETING - Campañas, presupuesto de marketing
builder.Services.AddHttpClient<IMarketingClient, MarketingClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Marketing"]
        ?? "http://localhost:5005");
});

// 6. ALMACÉN FÁBRICA - Inventario de fábrica, creación de pedidos
builder.Services.AddHttpClient<IAlmacenFabricaClient, AlmacenFabricaClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:AlmacenFabrica"]
        ?? "http://localhost:5006");
});


// Servicios de Aplicación
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<SolicitudesService>();
builder.Services.AddScoped<ReportesService>();

// Configuración API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization(); 

var app = builder.Build();

// 3. MIGRACIÓN AUTOMÁTICA 

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SucursalDbContext>();
    try
    {
        db.Database.Migrate();
        Console.WriteLine("--> Migración aplicada exitosamente en Railway.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--> Error aplicando migración: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
// app.UseHttpsRedirection(); // Comentado para evitar errores de conexión
app.UseAuthorization();
app.MapControllers();

app.Run();