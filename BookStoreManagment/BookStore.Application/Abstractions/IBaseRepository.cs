using System.Linq.Expressions;          // Expression ishlashi uchun

namespace BookStore.Application.Abstractions
{
    public interface IBaseRepository<T> where T : class
    {
        public string Create(T model);
        public IEnumerable<T> GetAll();
        public T GetByAny(Expression<Func<T, bool>> expression);
        public string Update(T model);
        public string Delete(Expression<Func<T, bool>> expression);
    }
}
