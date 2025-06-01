using CarShop.DataAccessLayer;
using CarShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace CarShop.Endpoints
{
    public static class CarsEndpoints
    {
        //add static method to the web application class
        public static async Task<RouteGroupBuilder> MapCarsEndpoints(this WebApplication app)
        {
            var routeGroup = app.MapGroup("cars").WithParameterValidation();//will send an error message for missing value on required fields

            //GET /cars
            routeGroup.MapGet("/", async (CarContext carContext) => await carContext.Cars.Include("CarType").ToListAsync());

            //GET Car by Id /cars/{id}
            routeGroup.MapGet("/{id}", async (CarContext carContext, int id) => {

                Car? car = await carContext.Cars.Include("CarType").FirstOrDefaultAsync(c => c.Id == id);
                return car is null ? Results.NotFound() : Results.Ok(car);
            });

            //POST /cars
            routeGroup.MapPost("/", async (Car newcar, CarContext carContext) =>
            {
                newcar.CarType = await carContext.CarTypes.FirstOrDefaultAsync(ct => ct.Id == newcar.CarTypeId);
                carContext.Cars.Add(newcar);
                await carContext.SaveChangesAsync();
                return Results.Created(string.Format("/cars/{0}", newcar.Id), newcar.Id);
            });

            //PUT /cars/{id}
            routeGroup.MapPut("/{id}", async (int id, Car newcar, CarContext carContext) =>
            {
                Car? car = await carContext.Cars.FindAsync(id);
                if(car is null)
                {
                    return Results.NotFound();
                }

                if(newcar.Brand is not null) { car.Brand = newcar.Brand; }
                if(newcar.Model is not null) { car.Model = newcar.Model; }
                if(newcar.Year != default) { car.Year = newcar.Year; }
                if(newcar.Color is not null) { car.Color = newcar.Color; }
                if(newcar.Price != 0) { car.Price = newcar.Price; }
                if(newcar.NumberOfDoors != 0) { car.NumberOfDoors = newcar.NumberOfDoors; }
                if(newcar.CarTypeId != 0) 
                { car.CarTypeId = newcar.CarTypeId; car.CarType = carContext?.CarTypes.Find(newcar.CarTypeId); }
                if(newcar.IsElectric != null) { car.IsElectric = newcar.IsElectric; }

                carContext.Cars.Update(car);
                await carContext.SaveChangesAsync();

                return Results.NoContent();
            });

            routeGroup.MapDelete("/{id}", async (int id, CarContext carContext) =>
            {
                Car? car = await carContext.Cars.FindAsync(id);
                if (car is null)
                {
                    return Results.NotFound();
                }
                carContext.Cars.Remove(car);
                await carContext.SaveChangesAsync();

                // indicates that the request was successfull,
                // but there is no information to send back in the response body
                return Results.NoContent();
            });

            return routeGroup;
        }

    }
}
