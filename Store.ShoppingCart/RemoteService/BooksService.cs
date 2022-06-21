using Store.ShoppingCart.RemoteInterface;
using Store.ShoppingCart.RemoteModel;
using System.Text.Json;

namespace Store.ShoppingCart.RemoteService
{
    public class BooksService : IBookService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BooksService> _logger;

        public BooksService(IHttpClientFactory httpClientFactory, ILogger<BooksService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }


        public async Task<(bool result, BookRemote book, string ErrorMessage)> GetLibro(Guid bookId)
        {
            try
            {
                var newBookId = bookId.ToString().ToUpper();
                var client = _httpClientFactory.CreateClient("Books");
                var response = await client.GetAsync($"api/LibraryMaterial/{bookId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<BookRemote>(content,options);
                    return (true,result,null);
                }

                return (false,null,response.ReasonPhrase);
            }catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false,null,ex.Message);
            }
        }
    }
}
