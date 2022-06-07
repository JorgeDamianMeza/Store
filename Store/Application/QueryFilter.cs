using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Model;
using Store.Persistence;

namespace Store.Application
{
    public class QueryFilter
    {
        public class SoleAuthor : IRequest<AuthorDto>
        {
            public string BookAuthorGuid { get; set; }
        }

        public class Handler : IRequestHandler<SoleAuthor, AuthorDto>
        {
            private readonly AuthorContext _authorContext ;
            private readonly IMapper _mapper;

            public Handler(AuthorContext authorContext,IMapper mapper)
            {
                _authorContext = authorContext;
                _mapper = mapper;
            }

            public async Task<AuthorDto> Handle(SoleAuthor request, CancellationToken cancellationToken)
            {
                var author = await _authorContext.BookAuthor.Where(x => x.BookAuthorGuid == request.BookAuthorGuid).FirstOrDefaultAsync();
                if (author == null) { throw new Exception("BookAuthor Not Found"); }

                var authorDto = _mapper.Map<BookAuthor,AuthorDto>(author);
                return authorDto;
            }
        }
    }
}
