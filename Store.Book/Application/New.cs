using FluentValidation;
using MediatR;
using Store.Book.Model;
using Store.Book.Persistence;

namespace Store.Book.Application
{
    public class New
    {
        public class Execute : IRequest
        {
            public string Title { get; set; }
            public DateTime? PublicacionDate { get; set; }
            public Guid? BookAuthor { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(c => c.Title).NotEmpty();
                RuleFor(c => c.PublicacionDate).NotEmpty();
                RuleFor(c => c.BookAuthor).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly LibraryContext _libraryContext;

            public Handler(LibraryContext libraryContext)
            {
                _libraryContext = libraryContext;
            }


            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var book = new LibraryMaterial
                {
                    Title = request.Title,
                    PublicationDate = request.PublicacionDate,
                    BookAuthor = request.BookAuthor
                };

                _libraryContext.LibraryMaterials.Add(book);
                var value = await _libraryContext.SaveChangesAsync();
                if(value > 0) { return Unit.Value; }

                throw new Exception("The book could not be saved");

            }
        }

    }
}
