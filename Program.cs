using Microsoft.EntityFrameworkCore;
using Sucursal.Core.Interfaces;
using Sucursal.Infraestructura.Clientes;
using Sucursal.Infraestructura.Data;
using Sucursal.Infraestructura.Repositorios;
using Sucursal.Infraestructura.Servicios;
// 1. Agrega esto PRIMERO de todo
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURACIÓN DEL PUERTO (OBLIGATORIO PARA RAILWAY)
// Tus notas dicen 8080 fijo, pero Railway asigna puertos dinámicos.
// Esta línea usa la variable PORT si existe (Nube), o el 8080 si es Local.
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Cargar appsettings
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// 2. BASE DE DATOS (SEGÚN TUS NOTAS)
// Leemos la variable "DATABASE" que pusiste en Railway.
// Si no existe (es null), usa la del appsettings (Local).
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

// 7. LOGÍSTICA - Rutas de distribución, envíos
builder.Services.AddHttpClient<ILogisticaClient, LogisticaClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Logistica"]
        ?? "http://localhost:5007");
});

// 8. PRODUCCIÓN - Órdenes de producción, capacidad de planta
builder.Services.AddHttpClient<IProduccionClient, ProduccionClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Produccion"]
        ?? "http://localhost:5008");
});

// 9. CONTROL DE CALIDAD - Inspecciones, retiros de producto
builder.Services.AddHttpClient<IControlCalidadClient, ControlCalidadClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:ControlCalidad"]
        ?? "http://localhost:5009");
});

// 10. MANTENIMIENTO - Equipos, paradas de máquinas
builder.Services.AddHttpClient<IMantenimientoClient, MantenimientoClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Mantenimiento"]
        ?? "http://localhost:5010");
});

// 11. SEGURIDAD - Incidentes, alertas de seguridad
builder.Services.AddHttpClient<ISeguridadClient, SeguridadClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Seguridad"]
        ?? "http://localhost:5011");
});

// 12. RECEPCIÓN - Entrada de materias primas
builder.Services.AddHttpClient<IRecepcionClient, RecepcionClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Recepcion"]
        ?? "http://localhost:5012");
});

// 13. DESPACHO - Salida de productos
builder.Services.AddHttpClient<IDespachoClient, DespachoClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Despacho"]
        ?? "http://localhost:5013");
});

// 14. ATENCIÓN AL CLIENTE - Reclamos, satisfacción
builder.Services.AddHttpClient<IAtencionClienteClient, AtencionClienteClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:AtencionCliente"]
        ?? "http://localhost:5014");
});

// 15. FACTURACIÓN - Documentos, impuestos
builder.Services.AddHttpClient<IFacturacionClient, FacturacionClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Facturacion"]
        ?? "http://localhost:5015");
});

// 16. TESORERÍA - Caja, flujo de efectivo
builder.Services.AddHttpClient<ITesoreriaClient, TesoreriaClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Tesoreria"]
        ?? "http://localhost:5016");
});

// 17. PLANIFICACIÓN - Demanda, proyecciones
builder.Services.AddHttpClient<IPlanificacionClient, PlanificacionClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MicroservicesUrls:Planificacion"]
        ?? "http://localhost:5017");
});

// Servicios de Aplicación
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<SolicitudesService>();
builder.Services.AddScoped<ReportesService>();

// Configuración API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization(); // <--- ¡NO BORRES ESTO! Evita que la app se rompa.

var app = builder.Build();

// 3. MIGRACIÓN AUTOMÁTICA (COMO DICEN TUS NOTAS)
// Esto ejecuta el "update database" solito cuando subes a la nube.
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