using System.Threading.Tasks;

namespace SenseHome.DataSync.Services
{
    public interface ISchedulerTask
    {
        Task Execute(object state);
    }
}
