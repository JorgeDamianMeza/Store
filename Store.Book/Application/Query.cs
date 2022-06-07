using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Book.Model;
using Store.Book.Persistence;

namespace Store.Book.Application
{
    public class Query
    {
        public class Execute : IRequest<List<LibraryMaterialDto>> { }

        public class Handler : IRequestHandler<Execute, List<LibraryMaterialDto>>
        {
            private readonly LibraryContext _context;

            private readonly IMapper _mapper;

            public Handler(LibraryContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<LibraryMaterialDto>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var books = await _context.LibraryMaterials.ToListAsync();
                var booksDto = _mapper.Map<List<LibraryMaterial>,List<LibraryMaterialDto>>(books);
                return booksDto;
            }
        }

    }
}
