using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Features.ToDoActivity.Commands.CreateActivity;
using ToDo.Application.Features.ToDoActivity.Commands.DeleteActivity;
using ToDo.Application.Features.ToDoActivity.Commands.UpdateActivity;
using ToDo.Application.Features.ToDoActivity.Queries.GetActivities;
using ToDo.Application.Features.ToDoActivity.Queries.GetActivityDetail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoActivityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoActivityController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET: api/<ToDoActivityController>
        [HttpGet]
        public async Task<List<GetActivitiesDto>> Get()
        {
            var items = await _mediator.Send(new GetActivitiesQuery());

            return items;
        }

        // GET api/<ToDoActivityController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetActivityDetailDto>> Get(int id)
        {
            var item = await _mediator.Send(new GetActivityDetailQuery(id));

            return item;
        }

        // POST api/<ToDoActivityController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateActivityCommandDto item)
        {
            var response = await _mediator.Send(item);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<ToDoActivityController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateActivityCommandDto item)
        {
            await _mediator.Send(item);
            return NoContent();
        }

        // DELETE api/<ToDoActivityController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteActivityCommandDto { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
