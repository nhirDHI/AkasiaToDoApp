using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Data;
using ToDo.Application.Contracts.Identity;
using ToDo.Application.Contracts.Infrastructure.Logging;
using ToDo.Application.Features.ToDoActivity.Commands.CreateActivity;
using ToDo.Application.MappingProfiles;
using ToDo.Application.UnitTests.Mocks;

namespace ToDo.Application.UnitTests.Features.Commands
{
    public class CreateActivityCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAppLogger<CreateActivityCommandHandler>> _mockLogger;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IToDoActivityRepository> _mockToDoActivityRepository;

        public CreateActivityCommandTest()
        {
            // Mock the dependencies
            _mockLogger = new Mock<IAppLogger<CreateActivityCommandHandler>>();
            _mockUserService = new Mock<IUserService>();
            _mockToDoActivityRepository = MockToDoActivityRepository.GetMockToDoActivityRepository();

            // Setup Mapper configuration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ToDoActivityProfiles>(); // Assuming ToDoActivityProfile is your AutoMapper profile
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidToDoActivity()
        {
            var handler = new CreateActivityCommandHandler(_mapper, _mockLogger.Object, _mockUserService.Object, _mockToDoActivityRepository.Object);

            await handler.Handle(new CreateActivityCommandDto()
            {
                Subject = "Subject 1",
                Description = "Description 1",
                IsDone = true,
                IsCanceled = false
            }, CancellationToken.None);

            var toDoActivities = await _mockToDoActivityRepository.Object.GetAsync();
            toDoActivities.Count.ShouldBe(3);
        }
    }
}
