using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<bool> SaveAsync()
        {
            return await dataContext.SaveChangesAsync() > 0;
        }
    }
}
