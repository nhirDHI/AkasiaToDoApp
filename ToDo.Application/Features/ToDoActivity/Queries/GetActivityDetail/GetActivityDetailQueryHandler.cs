using AutoMapper;
using MediatR;
using ToDo.Application.Contracts.Data;
using ToDo.Application.Contracts.Infrastructure.Logging;
using ToDo.Application.Exceptions;

namespace ToDo.Application.Features.ToDoActivity.Queries.GetActivityDetail
{
    public class GetActivityDetailQueryHandler : IRequestHandler<GetActivityDetailQuery, GetActivityDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetActivityDetailQueryHandler> _appLogger;
        private readonly IToDoActivityRepository _toDoActivityRepository;

        public GetActivityDetailQueryHandler(IMapper mapper, IAppLogger<GetActivityDetailQueryHandler> appLogger, IToDoActivityRepository toDoActivityRepository)
        {
            this._mapper = mapper;
            this._appLogger = appLogger;
            this._toDoActivityRepository = toDoActivityRepository;
        }

        public async Task<GetActivityDetailDto> Handle(GetActivityDetailQuery request, CancellationToken cancellationToken)
        {
            // Query the database
            var item = await _toDoActivityRepository.GetByIdAsync(request.id);

            // verify that record exists
            if (item == null)
                throw new NotFoundException(nameof(item), request.id);

            // convert data objects to DTO objects
            var data = _mapper.Map<GetActivityDetailDto>(item);

            // return list of DTO object
            return data;
        }
    }
}
