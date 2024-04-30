using AppDev2.Data;
using AppDev2.Repository.IRepository;

namespace AppDev2.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IApplicationRepository ApplicationRepository { get; private set; }
        public IJobListingRepository JobListingRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ApplicationRepository = new ApplicationRepository(context);
            JobListingRepository = new JobListingRepository(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
