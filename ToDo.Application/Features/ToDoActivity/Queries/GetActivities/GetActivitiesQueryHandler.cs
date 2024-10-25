using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Data;
using ToDo.Application.Contracts.Infrastructure.Logging;
using ToDo.Application.Features.ToDoActivity.Commands.UpdateActivity;

namespace ToDo.Application.Features.ToDoActivity.Queries.GetActivities
{
    public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, List<GetActivitiesDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetActivitiesQueryHandler> _appLogger;
        private readonly IToDoActivityRepository _toDoActivityRepository;

        public GetActivitiesQueryHandler(IMapper mapper, IAppLogger<GetActivitiesQueryHandler> appLogger, IToDoActivityRepository toDoActivityRepository)
        {
            this._mapper = mapper;
            this._appLogger = appLogger;
            this._toDoActivityRepository = toDoActivityRepository;
        }

        public async Task<List<GetActivitiesDto>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
        {
            // Query the database
            var items = await _toDoActivityRepository.GetAsync();

            // convert data objects to DTO objects
            var data = _mapper.Map<List<GetActivitiesDto>>(items);

            _appLogger.LogInformation("Items were retrieved successfully");

            // return list of DTO object
            return data;
        }
    }
}
