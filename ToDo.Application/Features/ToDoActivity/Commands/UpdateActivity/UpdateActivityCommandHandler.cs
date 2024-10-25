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
using ToDo.Application.Features.ToDoActivity.Commands.DeleteActivity;

namespace ToDo.Application.Features.ToDoActivity.Commands.UpdateActivity
{
    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommandDto, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<UpdateActivityCommandHandler> _appLogger;
        private readonly IToDoActivityRepository _toDoActivityRepository;

        public UpdateActivityCommandHandler(IMapper mapper, IAppLogger<UpdateActivityCommandHandler> appLogger, IToDoActivityRepository toDoActivityRepository)
        {
            this._mapper = mapper;
            this._appLogger = appLogger;
            this._toDoActivityRepository = toDoActivityRepository;
        }

        public async Task<Unit> Handle(UpdateActivityCommandDto request, CancellationToken cancellationToken)
        {
            // Validate incoming data
            var validator = new UpdateActivityCommandValidator(_toDoActivityRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                _appLogger.LogWarning("Validation errors in update request for {0} - {1}", nameof(ToDoActivity), request.Id);
                throw new BadRequestException("Invalid Item Code", validationResult);
            }

            // convert to domain entity object
            var itemToUpdate = _mapper.Map<Domain.Entities.ToDoActivity>(request);

            // add to database
            await _toDoActivityRepository.UpdateAsync(itemToUpdate.Id, itemToUpdate);

            // return Unit value
            return Unit.Value;
        }
    }
}
