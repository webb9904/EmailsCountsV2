namespace EmailCountsV2
{
    using NHibernate;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public IQueryable<T> Entity => _session.Query<T>();

        public T Get(int id)
        {
            return _session.Get<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _session.Query<T>().ToList();
        }

        public async Task Save(T entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task Delete(T entity)
        {
            await _session.DeleteAsync(entity);
        }

        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).SingleOrDefault();
        }

        public IEnumerable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return _session.Query<T>().Where(expression).ToList();
        }
    }
}
