namespace EmailCountsV2
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        void BeginTransaction();

        Task Commit();

        Task Rollback();

        void CloseTransaction();
    }
}
