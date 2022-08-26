using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Data.Repo.Interface;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        //GET api/city
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cities = await unitOfWork.CityRepository.GetCitiesAsync();
            var cityDto = mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(cityDto);
        }

        #region
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
        #endregion

        //POST api/city/add/city/post -- POST the data in JSON format
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = mapper.Map<City>(cityDto);
            unitOfWork.CityRepository.AddCity(city);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        {
            if (id != cityDto.Id)
                return BadRequest("Update Not Allowed");

            var cityFromDb = await unitOfWork.CityRepository.FindCity(id);

            if (cityFromDb == null)
                return BadRequest("Update Not Allowed");
            
            mapper.Map(cityDto, cityFromDb);
            await unitOfWork.SaveAsync();
            return StatusCode(200);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<City> cityToPatch)
        {
            var cityFromDb = await unitOfWork.CityRepository.FindCity(id);
            cityToPatch.ApplyTo(cityFromDb, ModelState);
            await unitOfWork.SaveAsync();
            return StatusCode(200);
        }

        //DELETE api/city/city/delete/id -- 
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            unitOfWork.CityRepository.DeleteCity(id);
            await unitOfWork.SaveAsync();
            return Ok(id);
        }
    }
}
