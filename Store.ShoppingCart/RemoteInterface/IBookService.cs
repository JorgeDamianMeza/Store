using Store.ShoppingCart.RemoteModel;

namespace Store.ShoppingCart.RemoteInterface
{
    public interface IBookService
    {
        Task<(bool result,BookRemote book, string ErrorMessage)> GetLibro(Guid bookId);
    }
}
