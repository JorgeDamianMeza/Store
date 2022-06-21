using Store.Geteway.BookRemote;
using Store.Geteway.InterfaceRemote;
using System.Text.Json;

namespace Store.Geteway.ImplementRemote
{
    public class AuthorRemote : IAuthorRemote
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AuthorRemote> _logger;

        public AuthorRemote(IHttpClientFactory httpClient,ILogger<AuthorRemote> logger)
        {
            _httpClientFactory = httpClient;
            _logger = logger;
        }

        public async Task<(bool result, AuthorModelRemote author, string ErrorMessage)> GetAuthor(Guid AuthorId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AuthorService");
                var response = await client.GetAsync($"/Author/{AuthorId}");

               var rs=  response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<AuthorModelRemote>(content,options);
                    return (true, result, null);                
                }
                return (false, null, response.ReasonPhrase);
            }catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return(false,null,ex.Message);
            }
        }
    }
}
