using System.ComponentModel.DataAnnotations;

namespace Store.ShoppingCart.Model
{
    public class SessionCart
    {
       
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }

        public ICollection<SessionCartDetail> DetailsList { get; set; }
    }
}
