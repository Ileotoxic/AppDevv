using AppDev2.Data;
using AppDev2.Models;
using AppDev2.Repository.IRepository;

namespace AppDev2.Repository
{
    public class JobListingRepository : Repository <JobListingModel>, IJobListingRepository
    {
        private readonly ApplicationDbContext _context;

        public JobListingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Phương thức để cập nhật đối tượng JobListing
        public void Update(JobListingModel JobListing)
        {
            // Sử dụng phương thức Update của DbSet để cập nhật đối tượng application trong DbContext
            _context.JobListings.Update(JobListing);
        }
    }
}
