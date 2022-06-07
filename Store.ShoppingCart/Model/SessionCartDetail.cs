namespace Store.ShoppingCart.Model
{
    public class SessionCartDetail
    {
        public int SessionCartDetailId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SelectedProduct { get; set; }
        public int SessionCartId { get; set; }
        public SessionCart SessionCart { get; set; }
    }
}
