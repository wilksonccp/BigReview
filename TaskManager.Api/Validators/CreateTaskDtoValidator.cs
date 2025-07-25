using FluentValidation;
using TaskManager.Api.DTOs;
namespace TaskManager.Api.Validators;


    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(x => x.IsCompleted)
                .NotNull().WithMessage("IsCompleted must be specified.");
        }
    }

