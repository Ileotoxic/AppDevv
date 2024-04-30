using AppDev2.Models;

namespace AppDev2.Repository.IRepository
{
    public interface IApplicationRepository : IRepository<ApplicationModel>
    {
        void Update(ApplicationModel application);
    }
}
