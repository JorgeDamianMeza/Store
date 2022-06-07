using Microsoft.EntityFrameworkCore;
using Store.Model;

namespace Store.Persistence
{
    public class AuthorContext : DbContext
    {
        public AuthorContext(DbContextOptions<AuthorContext> options) : base(options){ }

        public DbSet<BookAuthor> BookAuthor { get; set; }

        public DbSet<AcademicDegree> AcademicDegree { get; set; }
    }
}
