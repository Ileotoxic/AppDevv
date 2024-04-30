namespace AppDev2.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IApplicationRepository ApplicationRepository { get; }
        public IJobListingRepository JobListingRepository  { get; }
        void Save();
    }
}
