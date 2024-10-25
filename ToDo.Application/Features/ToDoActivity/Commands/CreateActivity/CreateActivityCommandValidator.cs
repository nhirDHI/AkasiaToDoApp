using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Data;

namespace ToDo.Application.Features.ToDoActivity.Commands.CreateActivity
{
    public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommandDto>
    {
        private readonly IToDoActivityRepository _toDoActivityRepository;

        public CreateActivityCommandValidator(IToDoActivityRepository toDoActivityRepository)
        {
            this._toDoActivityRepository = toDoActivityRepository;

            RuleFor(p => p.Subject)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull();
        }
    }
}
