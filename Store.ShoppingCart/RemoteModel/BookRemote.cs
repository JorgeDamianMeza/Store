namespace Store.ShoppingCart.RemoteModel
{
    public class BookRemote
    {
        public Guid? LibraryMaterialId { get; set; }

        public string Title { get; set; }

        public DateTime? PublicationDate { get; set; }

        public Guid? BookAuthor { get; set; }
    }
}
