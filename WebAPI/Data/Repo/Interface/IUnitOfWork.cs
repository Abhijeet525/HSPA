using System.Threading.Tasks;

namespace WebAPI.Data.Repo.Interface
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
        Task<bool> SaveAsync();
    }
}
