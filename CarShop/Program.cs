using CarShop.DataAccessLayer;
using CarShop.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//use connection string from appsettings json file
builder.Services.AddDbContext<CarContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("localMovieDb"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CarContext>();
        await context.Database.MigrateAsync();//add seed data into database (creates database if it not exists)
    }
    catch (Exception ex) 
    {
        Console.WriteLine(string.Format("Error during migration of the database: {0}", ex.Message));
    }
}

app.MapGet("/", () => "Hello World!");
app.MapCarsEndpoints();

app.Run();
