using MediatR;
using Store.ShoppingCart.Model;
using Store.ShoppingCart.Persistence;

namespace Store.ShoppingCart.Application
{
    public class New
    {
        public class Execute : IRequest
        {
            public DateTime SessionDateTime { get; set; }
            public List<string> ListProducts { get; set; }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly CartContext _context;

            public Handler(CartContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var sessionCart = new SessionCart
                {
                    CreatedDate = request.SessionDateTime
                };
                _context.SessionCart.Add(sessionCart);
                var value = await _context.SaveChangesAsync();
                if(value == 0) { throw new Exception("Error in the insertion of the shopping cart"); }

                int id = sessionCart.Id;

                foreach(var obj in request.ListProducts)
                {
                    var sessionDetail = new SessionCartDetail
                    {
                        CreatedDate = DateTime.Now,
                        SessionCartId = id,
                        SelectedProduct = obj
                    };
                    _context.SessionCartDetail.Add(sessionDetail);
                }

               value = await _context.SaveChangesAsync();
                if (value > 0) { return Unit.Value; }
                throw new Exception("Failed to insert shopping cart detail");

            }
        }
    }
}
