using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace CarShop.Models
{
    public class Car
    {
        // Properties of the Car class

        //the primary key
        public int Id { get; set; }

        //Gets or sets the Brand of the car (e.g., "Toyota", "Ford").
        [Required]
        public string? Brand { get; set; } = string.Empty;

        //Gets or sets the model of the car (e.g., "Camry", "F-150").
        [Required]
        public string? Model { get; set; } = string.Empty; // set default value not to receive 400 for missing value on update/PUT method 

        //Gets or sets the ID of the type of the car (e.g., 1, 2).
        public int CarTypeId { get; set; }

        //Gets or sets the type of the car (e.g., "Sedan", "SUV").
        //don't need to validate the existence of an CarType object,
        //is required only a CarType Id for this object
        //Navigation Property
        [ValidateNever]        
        public CarType CarType { get; set; }

        //Gets or sets the manufacturing year of the car.        
        public int Year { get; set; }

        //Gets or sets the color of the car. (e.g. "Red", "Blue")
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)] //ignore if null 
        public string? Color { get; set; }

        //Gets or sets the price of the car, since the price can change in time (e.g. 120000M)        
        [Required] //mandatory field
        [Range(1, 100)]
        public decimal? Price { get; set; } = decimal.One;
        
        //Gets or sets the number of doors the car has. (e.g. 4, 2)      
        public int NumberOfDoors { get; set; }

        //Gets or sets a boolean indicating if the car is electric.
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)] //ignore if null on post
        public bool IsElectric { get; set; }

    }
}
