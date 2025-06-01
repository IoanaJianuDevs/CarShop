using CarShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShop.DataAccessLayer
{
    /// <summary>
    ///CarContext class takes a datatype ContextOptions with the variable identifier option
    /// and it passes it to parent constructor (DbContext) - DBContext represents a session with the database (acting as a gateway for interacting with DB)
    /// </summary>
    /// <param name="options"></param>
    public class CarContext(DbContextOptions<CarContext> options) : DbContext(options)
    {
        //create database tables
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarType> CarTypes { get; set; }

        //add the seed data into database for when there is no data into database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarType>().HasData(
                new CarType { Id = 1, Name = "Sedan" },
                new CarType { Id = 2, Name = "SUV(SportUtilityVehicle)" },
                new CarType { Id = 3, Name = "Hatchback" },
                new CarType { Id = 4, Name = "Coupe" },
                new CarType { Id = 5, Name = "CompactCar" }
                );

            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Brand = "Honda", Model = "Civic", CarTypeId = 5, Year = 2022, Color = "Blue", Price = 15000.5M, NumberOfDoors = 4, IsElectric = false },
                new Car { Id = 2, Brand = "Tesla", Model = "Model 3", CarTypeId = 1, Year = 2024, Color = "Red", Price = 500.0M, NumberOfDoors = 4, IsElectric = true },
                new Car { Id = 3, Brand = "Ford", Model = "Mustang", CarTypeId = 4, Year = 2023, Color = "Black", Price = 2000.0M, NumberOfDoors = 2, IsElectric = false },
                new Car { Id = 4, Brand = "Toyota", Model = "RAV4", CarTypeId = 2, Year = 2021, Color = "Silver", Price = 30000.0M, NumberOfDoors = 4, IsElectric = false }
                );
    }
        
}       

    #region constructor for CarContext 
    //we choose option Use primary constructor
    //and our class will take the parameters that the contructor required before choosing
    //will pass the options directly to DBContext (base naming was replaced with the actual name of the base class)
    ///// <summary>
    ///// /constructor for CarContext class takes a datatype ContextOptions with the variable identifier option
    ///// and it passes it to parent constructor (DbContext)
    ///// </summary>
    ///// <param name="options"></param>
    //public CarContext(DbContextOptions<CarContext> options) : base(options)
    //{

    //}
    #endregion
}
