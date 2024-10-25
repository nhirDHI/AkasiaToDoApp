using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Data;
using ToDo.Application.Contracts.Infrastructure.Logging;
using ToDo.Application.Exceptions;
using ToDo.Application.Features.ToDoActivity.Commands.CreateActivity;

namespace ToDo.Application.Features.ToDoActivity.Commands.DeleteActivity
{
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommandDto, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<DeleteActivityCommandHandler> _appLogger;
        private readonly IToDoActivityRepository _toDoActivityRepository;

        public DeleteActivityCommandHandler(IMapper mapper, IAppLogger<DeleteActivityCommandHandler> appLogger, IToDoActivityRepository toDoActivityRepository)
        {
            this._mapper = mapper;
            this._appLogger = appLogger;
            this._toDoActivityRepository = toDoActivityRepository;
        }

        public async Task<Unit> Handle(DeleteActivityCommandDto request, CancellationToken cancellationToken)
        {
            // retrieve domain entity object
            var itemToDelete = await _toDoActivityRepository.GetByIdAsync(request.Id);

            // verify that record exists
            if (itemToDelete == null)
                throw new NotFoundException(nameof(ToDoActivity), request.Id);

            // remove from database
            await _toDoActivityRepository.DeleteAsync(itemToDelete.Id);

            // retun record id
            return Unit.Value;
        }
    }
}
