﻿using FoodTruckChallenge;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace food_truck_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodTruckController : ControllerBase
    {
        private IFoodTruckRepository _foodTruckRepository;
        private FoodTruckManager _foodTruckManager;
        public FoodTruckController(IFoodTruckRepository foodTruckRepository, FoodTruckManager foodTruckManager)
        {
            _foodTruckRepository = foodTruckRepository;
            _foodTruckManager = foodTruckManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var task = await Task.Run(() =>
            {
                return _foodTruckRepository.GetFoodTruckFacilities();
            });
            
            return Ok(task);
        }


        [HttpGet]
        [Route("location")]
        public async Task<IActionResult> Get(double? lat, double? lon, double radius = 0.1)
        {
            if(lat == null || lon == null)
            {
                return BadRequest("Invalid input");
            }


            var task = await Task.Run(() =>
            {
                //You can also request Device location based data instead of askign user for the lat/long input
                return _foodTruckManager.GetFoodTrucksWithinArea(lat.Value, lon.Value, radius);
            });

            return Ok(task);
        }
    }
}
