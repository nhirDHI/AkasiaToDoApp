using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Data;
using ToDo.Application.Contracts.Identity;
using ToDo.Application.Contracts.Infrastructure.Logging;
using ToDo.Application.Exceptions;
using ToDo.Application.Features.ToDoActivity.Commands.UpdateActivity;

namespace ToDo.Application.Features.ToDoActivity.Commands.CreateActivity
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommandDto, int>
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateActivityCommandHandler> _appLogger;
        private readonly IUserService _userService;
        private readonly IToDoActivityRepository _toDoActivityRepository;

        public CreateActivityCommandHandler(IMapper mapper, IAppLogger<CreateActivityCommandHandler> appLogger, IUserService userService, IToDoActivityRepository toDoActivityRepository)
        {
            this._mapper = mapper;
            this._appLogger = appLogger;
            this._userService = userService;
            this._toDoActivityRepository = toDoActivityRepository;
        }

        public async Task<int> Handle(CreateActivityCommandDto request, CancellationToken cancellationToken)
        {
            // Validate incoming data
            var validator = new CreateActivityCommandValidator(_toDoActivityRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                _appLogger.LogWarning("Validation errors in update request for {0} - {1}", nameof(ToDoActivity), request.Subject);
                throw new BadRequestException("Invalid Content Type", validationResult);
            }

            

            // Convert to domain entity object
            var itemToCreate = _mapper.Map<Domain.Entities.ToDoActivity>(request);

            // Get latest Activity Code 
            var latestActivity = await _toDoActivityRepository.GetLatestActivityAsync();
            var nextActivityNumber = 1;
            if (latestActivity != null)
            {
                // Assuming the format is AC-XXXX, extract the number part and increment
                var lastActivityCode = latestActivity.ActivityCode;
                if (int.TryParse(lastActivityCode.Substring(3), out int lastNumber))
                {
                    nextActivityNumber = lastNumber + 1;
                }
            }

            // Format the new ActivityCode as "AC-XXXX"
            itemToCreate.ActivityCode = $"AC-{nextActivityNumber.ToString("D4")}";

            // Add to database
            await _toDoActivityRepository.CreateAsync(itemToCreate);

            // return record id
            return itemToCreate.Id;
        }
    }
}
