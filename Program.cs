using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using DevExpress.XtraReports.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using reporte.Data;
using reporte.Services;
using static DevExpress.Office.Drawing.LazyGroupBrush;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorPages()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Registre los servicios de informes en el contenedor de inyección de dependencia de la aplicación.
builder.Services.AddDevExpressControls();
// Registra el almacenamiento después de la llamada al método AddDevExpressControls.
builder.Services.AddScoped<ReportStorageWebExtension, CustomReportStorageWebExtension>();
builder.Services.AddDbContext<ReportDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ReportsDataConnectionString")));

var app = builder.Build();

app.UseDevExpressControls();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

//genero la base de datos si no existe
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ReportDbContext>();
    context.Database.EnsureCreated();
    
    //ejecuta la inserción de datos semillas seeders
    DbInitializer.Initialize(context);
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
