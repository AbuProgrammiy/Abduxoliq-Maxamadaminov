using BookStore.Domain.Entities.Models;     // User, Book, Order ishlashi uchun
using Microsoft.EntityFrameworkCore;        // DbContext, DbSet ishlashi uchun

namespace BookStore.Infrastructure.Persistance
{
    public class BookStoreDbContext: DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options):base(options)
        {
            
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}
