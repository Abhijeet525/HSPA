using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Data.Repo.Interface;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CityController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //GET api/city
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await unitOfWork.CityRepository.GetCitiesAsync();
            return Ok(cities);
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
        public async Task<IActionResult> AddCity(City city)
        {
            unitOfWork.CityRepository.AddCity(city);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
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
