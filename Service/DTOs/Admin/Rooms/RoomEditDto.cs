using FluentValidation;

namespace Service.DTOs.Admin.Rooms
{
    public class RoomEditDto
    {
        public string Name { get; set; }
        public int SeatCount { get; set; }
    }

    public class RoomEditDtoValidator : AbstractValidator<RoomEditDto>
    {
        public RoomEditDtoValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name cannot exceed 50 characters");

            RuleFor(m => m.SeatCount)
                .NotEmpty()
                .WithMessage("Seat count is required")
                .GreaterThan(0)
                .WithMessage("Seat count must be grater than 0");
        }
    }
}
