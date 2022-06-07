using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.ShoppingCart.Persistence;
using Store.ShoppingCart.RemoteInterface;

namespace Store.ShoppingCart.Application
{
    public class Query
    {
        public class Execute : IRequest<ShoppingCartDto>          
        {
            public int ShoppingId { get; set; }
        }

        public class Handler : IRequestHandler<Execute, ShoppingCartDto>
        {
            private readonly CartContext _cartContext;
            private readonly IBookService _bookService;           

            public Handler(CartContext cartContext, IBookService bookService)
            {
                _cartContext = cartContext;
                _bookService = bookService;
            }


            public async Task<ShoppingCartDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var cartSession = await _cartContext.SessionCart.FirstOrDefaultAsync(x => x.Id == request.ShoppingId);
                var cartSessionDetail = await _cartContext.SessionCartDetail.Where(x => x.SessionCartId == request.ShoppingId).ToListAsync();
                var listCartDto = new List<ShoppingCartDetailDto>();
                foreach (var book in cartSessionDetail)
                {
                   var response = await _bookService.GetLibro(new Guid (book.SelectedProduct));
                    if (response.result)
                    {
                        var objectBook = response.book;
                        var cartDetail = new ShoppingCartDetailDto
                        {
                            BookTitle = objectBook.Title,
                            PublicationDate = (DateTime)objectBook.PublicationDate,
                            BookId = objectBook.LibraryMaterialId
                        };
                        listCartDto.Add(cartDetail);
                    }
                }

                var cartSessionDto = new ShoppingCartDto
                {
                    ShoppingId = cartSession.Id,
                    DateCreatedSession = cartSession.CreatedDate,
                    ListProducts = listCartDto                    
                };

                return cartSessionDto;

            }
        }

    }   

}
    
    

