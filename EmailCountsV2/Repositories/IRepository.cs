namespace EmailCountsV2
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        T Get(int id);

        IEnumerable<T> GetAll();

        Task Save(T entity);

        Task Delete(T entity);

        T FindBy(Expression<Func<T, bool>> expression);

        IEnumerable<T> FilterBy(Expression<Func<T, bool>> expression);
    }
}
