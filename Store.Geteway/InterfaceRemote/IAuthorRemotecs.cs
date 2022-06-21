using Store.Geteway.BookRemote;

namespace Store.Geteway.InterfaceRemote
{
    public interface IAuthorRemote
    {
        Task<(bool result, AuthorModelRemote author, string ErrorMessage)> GetAuthor(Guid AuthorId);
    }
}
