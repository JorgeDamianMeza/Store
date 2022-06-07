namespace Store.ShoppingCart.Application
{
    public class ShoppingCartDto
    {
        public int ShoppingId { get; set; }
        public DateTime? DateCreatedSession { get; set; }
        public List<ShoppingCartDetailDto> ListProducts { get; set; }
    }
}
