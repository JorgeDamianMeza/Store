using Microsoft.EntityFrameworkCore;
using Store.Book.Model;

namespace Store.Book.Persistence
{
    public class LibraryContext : DbContext
    {

        public LibraryContext() { }

        public LibraryContext(DbContextOptions<LibraryContext>options): base(options) { }

        public virtual DbSet<LibraryMaterial> LibraryMaterials { get; set; }
    }
}
