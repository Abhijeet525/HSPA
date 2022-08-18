using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Data.Repo.Interface;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext dataContext;
        public CityRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public void AddCity(City city)
        {
            dataContext.AddAsync(city);
        }

        public void DeleteCity(int cityId)
        {
            var city = dataContext.Cities.Find(cityId);
            dataContext.Cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await dataContext.Cities.ToListAsync();
        }
    }
}
