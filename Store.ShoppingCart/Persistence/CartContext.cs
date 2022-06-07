using Microsoft.EntityFrameworkCore;
using Store.ShoppingCart.Model;

namespace Store.ShoppingCart.Persistence
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext>options) : base(options) { }

        public DbSet<SessionCart> SessionCart { get; set; }

        public DbSet<SessionCartDetail> SessionCartDetail { get; set; }
    }
}
