using FluentValidation;
using MediatR;
using Store.Model;
using Store.Persistence;

namespace Store.Application
{
    public class New
    {
        public class Execute : IRequest
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime? DateOfBrith { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Surname).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            public readonly AuthorContext _authorContext;

            public Handler(AuthorContext authorContext)
            {
                _authorContext = authorContext;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var bookAuthor = new BookAuthor
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    DateOfBirth = request.DateOfBrith,
                    BookAuthorGuid = Convert.ToString(Guid.NewGuid())
                };
                _authorContext.BookAuthor.Add(bookAuthor);
                var value = await _authorContext.SaveChangesAsync();

                if (value > 0) { return Unit.Value; }

                throw new Exception("Could not insert BookAuthor");

            }
        }
    }
}
