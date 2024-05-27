using FluentValidation;

namespace Service.DTOs.Admin.Groups
{
    public class GroupEditDto
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int EducationId { get; set; }
        public int RoomId { get; set; }
    }

    public class GroupEditDtoValidator : AbstractValidator<GroupEditDto>
    {
        public GroupEditDtoValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters");

            RuleFor(m => m.Capacity)
                .NotEmpty()
                .WithMessage("Capacity is required")
                .GreaterThan(0)
                .WithMessage("Capacity must be grater than 0");

            RuleFor(m => m.EducationId)
                .NotEmpty()
                .WithMessage("Education id is required")
                .GreaterThan(0)
                .WithMessage("Education id must be grater than 0");

            RuleFor(m => m.RoomId)
                .NotEmpty()
                .WithMessage("Room id is required")
                .GreaterThan(0)
                .WithMessage("Room id must be grater than 0");
        }
    }
}
