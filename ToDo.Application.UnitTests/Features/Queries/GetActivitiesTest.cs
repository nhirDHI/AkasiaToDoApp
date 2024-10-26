using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Data;
using ToDo.Application.Contracts.Infrastructure.Logging;
using ToDo.Application.Features.ToDoActivity.Queries.GetActivities;
using ToDo.Application.MappingProfiles;
using ToDo.Application.UnitTests.Mocks;

namespace ToDo.Application.UnitTests.Features.Queries
{
    public class GetActivitiesTest
    {
        private readonly Mock<IToDoActivityRepository> _mockRepo;
        private IMapper _mapper;
        private Mock<IAppLogger<GetActivitiesQueryHandler>> _mockAppLogger;

        public GetActivitiesTest()
        {
            _mockRepo = MockToDoActivityRepository.GetMockToDoActivityRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ToDoActivityProfiles>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockAppLogger = new Mock<IAppLogger<GetActivitiesQueryHandler>>();
        }

        [Fact]
        public async Task GetLeaveTypeListTest()
        {
            var handler = new GetActivitiesQueryHandler(_mapper, _mockAppLogger.Object, _mockRepo.Object);

            var result = await handler.Handle(new GetActivitiesQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<GetActivitiesDto>>();
            result.Count.ShouldBe(2);
        }
    }
}
