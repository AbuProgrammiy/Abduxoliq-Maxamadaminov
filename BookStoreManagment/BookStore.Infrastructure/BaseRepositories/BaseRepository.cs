using BookStore.Application.Abstractions;       // DbSet ishlashi uchun
using BookStore.Infrastructure.Persistance;     // BookStoreDbContext ishlashi uchun
using Microsoft.EntityFrameworkCore;            // DbSet ishlashi uchun
using System.Linq.Expressions;                  // Expression ishlashi uchun
using Microsoft.EntityFrameworkCore.Storage;

namespace BookStore.Infrastructure.BaseRepositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly BookStoreDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(BookStoreDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public string Create(T model)
        {
            // Prezentatsiyam davomida Sarvaraka etkanlaridek Tranzaksiyar EFCoreda ishlatish ortiqcha ishligini bilib oldim
            // 
            // 
            using IDbContextTransaction? transaction =  _context.Database.BeginTransactionAsync().GetAwaiter().GetResult();
            try
            {
                _dbSet.AddAsync(model).GetAwaiter().GetResult();
                _context.SaveChangesAsync().GetAwaiter().GetResult();

                transaction.CommitAsync().GetAwaiter().GetResult();
                return "Create qilindi!";
            }
            catch
            {
                transaction.RollbackAsync().GetAwaiter().GetResult();
                return "Xatolik yuzberdi,\nAmmo qorqmang, Hammasi nazorat ostida";
            }
        }

        public string Delete(Expression<Func<T, bool>> expression)
        {
            using IDbContextTransaction? transaction = _context.Database.BeginTransactionAsync().GetAwaiter().GetResult();
            try
            {
                T model = _dbSet.FirstOrDefaultAsync(expression).GetAwaiter().GetResult()!;
                if (model == null)
                {
                    return "Ma'lumot topilmadi.\nShuning uchun hechnima ham ochirilmadi)";
                }
                _dbSet.Remove(model);
                _context.SaveChangesAsync().GetAwaiter().GetResult();

                transaction.CommitAsync().GetAwaiter().GetResult();
                return "Malumot ochirildi!";
            }
            catch
            {
                transaction.RollbackAsync().GetAwaiter().GetResult();
                return "Xatolik yuzberdi,\nAmmo qorqmang, Hammasi nazorat ostida";
            }
        }

        public IEnumerable<T> GetAll()
        {
            
            using IDbContextTransaction? transaction = _context.Database.BeginTransactionAsync().GetAwaiter().GetResult();
            try
            {
                IEnumerable<T>  Data = _dbSet.ToListAsync().GetAwaiter().GetResult();
                transaction.CommitAsync().GetAwaiter().GetResult();
                return Data;
            }
            catch(Exception ex)
            {
                transaction.RollbackAsync();
                throw ex;
            }
        }

        public T GetByAny(Expression<Func<T, bool>> expression)
        {
            using IDbContextTransaction? transaction = _context.Database.BeginTransactionAsync().GetAwaiter().GetResult();
            try
            {
                T Data = _dbSet.FirstOrDefault(expression)!;
                transaction.CommitAsync().GetAwaiter().GetResult();
                return Data;
            }
            catch(Exception ex)
            {
                transaction.RollbackAsync();
                throw ex;
            }
        }

        public string Update(T model)
        {
            using IDbContextTransaction? transaction = _context.Database.BeginTransactionAsync().GetAwaiter().GetResult();
            try
            {
                _dbSet.Update(model);
                _context.SaveChangesAsync().GetAwaiter().GetResult();

                transaction.CommitAsync().GetAwaiter().GetResult();
                return "Update muvaffaqiyat topdi!";
            }
            catch(Exception ex)
            {
                transaction?.RollbackAsync();
                return "Xatolik yuzberdi,\nAmmo qorqmang, Hammasi nazorat ostida";
            }
        }
    }
}
