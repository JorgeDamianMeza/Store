using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Model;
using Store.Persistence;

namespace Store.Application
{
    public class Query
    {
        public class AuthorList : IRequest<List<AuthorDto>> { }

        public class Handler : IRequestHandler<AuthorList, List<AuthorDto>>
        {
            private readonly AuthorContext _authorContext;
            private readonly IMapper _mapper;

            public Handler(AuthorContext authorContext,IMapper mapper)
            {
                _authorContext = authorContext;
                _mapper = mapper;
            }

            public async Task<List<AuthorDto>> Handle(AuthorList request, CancellationToken cancellationToken)
            {
               var authors = await _authorContext.BookAuthor.ToListAsync();
                var authorsDto = _mapper.Map<List<BookAuthor>, List<AuthorDto>>(authors);
                return authorsDto;
            }
        }
    }
}
