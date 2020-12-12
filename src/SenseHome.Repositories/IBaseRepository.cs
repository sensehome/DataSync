using System.Threading.Tasks;

namespace SenseHome.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T entity);
    }
}
