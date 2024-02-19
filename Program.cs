using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using reporte.Data;
using reporte.DataSetSp;
using reporte.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorPages()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Registre los servicios de informes en el contenedor de inyecci�n de dependencia de la aplicaci�n.
builder.Services.AddDevExpressControls();
// Registra el almacenamiento despu�s de la llamada al m�todo AddDevExpressControls.
builder.Services.AddScoped<ReportStorageWebExtension, CustomReportStorageWebExtension>();
builder.Services.AddDbContext<ReportDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ReportsDataConnectionString")));

//registro inyecci�n de dependencia de usar la configuraci�n.
/* builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddSingleton<sp>(); */

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
    
    //ejecuta la inserci�n de datos semillas seeders
    DbInitializer.Initialize(context);
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
