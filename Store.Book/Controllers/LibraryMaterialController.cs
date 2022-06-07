using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Book.Application;

namespace Store.Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryMaterialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibraryMaterialController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<Unit>> Create(New.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibraryMaterialDto>>> GetBooks()
        {
            return await _mediator.Send(new Query.Execute());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryMaterialDto>> GetBook(Guid id)
        {
            return await _mediator.Send(new QueryFilter.UniqueBook { BookId = id });
        }


    }
}
