using System.Linq.Expressions;

namespace Contracts.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        void Create(T item);
        Task<T>? FindById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        IEnumerable<T> Get();
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Remove(T item);
        void Update(T item);
        Task<IEnumerable<T>> GetByConditions(Dictionary<string, string> conditions);

    }
}
