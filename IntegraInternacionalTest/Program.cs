using DataAccess;
using DataAccess.Helpers;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Services.Services;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options=>options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

//builder.Services.AddSingleton<IWebHostEnvironment>(provider=> builder.Environment);
builder.Services.AddScoped<RootHelper>();
builder.Services.AddScoped<EntityHelper>();
builder.Services.AddScoped<IEmpleadoRepository,EmpleadoRepository>();
builder.Services.AddScoped<IEmpleadoServices,EmpleadoServices>();
builder.Services.AddControllersWithViews();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

using var scope = app.Services.CreateScope();
using(var ctx =scope.ServiceProvider.GetService<AppDbContext>())
{
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
    ctx.Database.Migrate();
}
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
