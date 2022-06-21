namespace Store.Geteway.BookRemote
{
    public class BookModelRemote
    {
        public Guid? LibraryMaterialId { get; set; }

        public string Title { get; set; }

        public DateTime? PublicationDate { get; set; }

        public Guid? BookAuthor { get; set; }

        public AuthorModelRemote AuthorData { get; set; }
    }
}
