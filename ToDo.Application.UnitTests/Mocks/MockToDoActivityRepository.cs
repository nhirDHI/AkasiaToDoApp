using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Data;
using ToDo.Domain.Entities;

namespace ToDo.Application.UnitTests.Mocks
{
    public class MockToDoActivityRepository
    {
        public static Mock<IToDoActivityRepository> GetMockToDoActivityRepository()
        {
            var toDoActivities = new List<ToDoActivity>
            {
                new ToDoActivity
                {
                    ActivityCode = "AC-0001",
                    Subject = "Subject 1",
                    Description = "Description 1",
                    IsDone = true,
                    IsCanceled = false
                },
                new ToDoActivity
                {
                    Id = 1,
                    ActivityCode = "AC-0002",
                    Subject = "Subject 2",
                    Description = "Description 2",
                    IsDone = true,
                    IsCanceled = false
                },
            };

            var mockRepo = new Mock<IToDoActivityRepository>();

            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(toDoActivities);

            mockRepo.Setup(r => r.CreateAsync(It.IsAny<ToDoActivity>()))
                .Returns((ToDoActivity leaveType) =>
                {
                    toDoActivities.Add(leaveType);
                    return Task.CompletedTask;
                });

            return mockRepo;
        }
    }
}
