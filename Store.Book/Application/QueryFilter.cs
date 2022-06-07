using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Book.Model;
using Store.Book.Persistence;

namespace Store.Book.Application
{
    public class QueryFilter
    {
        public class UniqueBook : IRequest<LibraryMaterialDto>
        {
            public Guid? BookId { get; set; }
        }

        public class Handler : IRequestHandler<UniqueBook, LibraryMaterialDto>
        {
            private readonly LibraryContext _context;
            private readonly IMapper _mapper;


            public Handler(LibraryContext context,IMapper mapper) { _context = context; _mapper = mapper; }


            public async Task<LibraryMaterialDto> Handle(UniqueBook request, CancellationToken cancellationToken)
            {
                var book = await _context.LibraryMaterials.Where(x => x.LibraryMaterialId == request.BookId).FirstOrDefaultAsync();
                if (book == null) { throw new Exception("Book not found"); }

                var bookDto = _mapper.Map<LibraryMaterial,LibraryMaterialDto>(book);
                return bookDto;
            }
        }


    }
}
