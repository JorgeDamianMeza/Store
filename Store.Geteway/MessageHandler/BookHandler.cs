using Store.Geteway.BookRemote;
using Store.Geteway.InterfaceRemote;
using System.Diagnostics;
using System.Text.Json;

namespace Store.Geteway.MessageHandler
{
    public class BookHandler : DelegatingHandler
    {
        private readonly ILogger<BookHandler> _logger;

        private readonly IAuthorRemote _authorRemote;

        public BookHandler(ILogger<BookHandler> logger,IAuthorRemote authorRemote)
        {
            _logger = logger;
            _authorRemote = authorRemote;
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var time = Stopwatch.StartNew();
            _logger.LogInformation("Start request");
            var response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var optiones = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<BookModelRemote>(content, optiones);
                var responseAuthor = await _authorRemote.GetAuthor(result.BookAuthor ?? Guid.Empty);
                if (responseAuthor.result)
                {
                    var objAuthor = responseAuthor.author;
                    result.AuthorData = objAuthor;
                    var resultStr = JsonSerializer.Serialize(result);
                    response.Content = new StringContent(resultStr,System.Text.Encoding.UTF8,"applicaction/json");
                }
            }

            _logger.LogInformation($"This process was done in {time.ElapsedMilliseconds} ms");

            return response;
        }
    }
}
