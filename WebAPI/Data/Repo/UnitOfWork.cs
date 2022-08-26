using System.Threading.Tasks;
using WebAPI.Data.Repo.Interface;

namespace WebAPI.Data.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dataContext;
        public UnitOfWork(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public ICityRepository CityRepository => new CityRepository(dataContext);

        public IUserRepository UserRepository => new UserRepository(dataContext);

        public async Task<bool> SaveAsync()
        {
            return await dataContext.SaveChangesAsync() > 0;
        }
    }
}
