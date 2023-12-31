using System.Reflection;
using autolog.Helper;
using autolog.Middleware;


var builder = WebApplication.CreateBuilder(args);

DbHelper dbHelper = new DbHelper(builder.Configuration);
dbHelper.CreateLogManagerClassFromDb();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

/* if (app.Environment.IsDevelopment()) app.UseLogTypeBuilder(); */

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
