using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Application;
using Store.Model;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Post(New.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAuthors()
        {
            return await _mediator.Send(new Query.AuthorList());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(string id)
        {
            return await _mediator.Send(new QueryFilter.SoleAuthor { BookAuthorGuid = id });
        }


    }
}
