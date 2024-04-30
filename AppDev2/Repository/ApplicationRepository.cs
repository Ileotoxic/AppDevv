using AppDev2.Data;
using AppDev2.Models;
using AppDev2.Repository.IRepository;
namespace AppDev2.Repository
{
    public class ApplicationRepository : Repository <ApplicationModel>, IApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Phương thức để cập nhật đối tượng ApplicationModel
        public void Update(ApplicationModel application)
        {
            // Sử dụng phương thức Update của DbSet để cập nhật đối tượng application trong DbContext
            _context.Applications.Update(application);
        }
    }
}
