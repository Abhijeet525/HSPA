using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.Repo;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository repo;

        public CityController(ICityRepository repo)
        {
            this.repo = repo;
        }

        //GET api/city
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await repo.GetCitiesAsync();
            return Ok(cities);
        }

        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "Atlanta";
        //}

        //POST api/city/add?cityName=Miami
        //POST api/city/add/Los Angeles
        //[HttpPost("add")]
        //[HttpPost("add/{cityName}")]
        //public async Task<IActionResult> AddCity(string cityName)
        //{
        //    City city = new City();
        //    city.Name = cityName;
        //    await dataContext.Cities.AddAsync(city);
        //    await dataContext.SaveChangesAsync();
        //    return Ok(city);
        //}

        //POST api/city/add/city/post -- POST the data in JSON format
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(City city)
        {
            repo.AddCity(city);
            await repo.SaveAsync();
            return StatusCode(201);
        }

        //DELETE api/city/city/delete/id -- 
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            repo.DeleteCity(id);
            await repo.SaveAsync();
            return Ok(id);
        }
    }
}
