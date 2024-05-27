using FluentValidation;

namespace Service.DTOs.Admin.Students
{
    public class StudentEditDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }

    public class StudentEditDtoValidator : AbstractValidator<StudentEditDto>
    {
        public StudentEditDtoValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name cannot exceed 50 characters");

            RuleFor(m => m.Surname)
                .NotEmpty()
                .WithMessage("Surname is required")
                .MaximumLength(50)
                .WithMessage("Surname cannot exceed 50 characters");

            RuleFor(m => m.Address)
                .NotEmpty()
                .WithMessage("Address is required")
                .MaximumLength(100)
                .WithMessage("Address cannot exceed 50 characters");


            RuleFor(m => m.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is invalid")
                .MaximumLength(50)
                .WithMessage("Email can be max 50 characters");

            RuleFor(m => m.Age)
                .NotEmpty()
                .WithMessage("Age is required")
                .GreaterThan(0)
                .WithMessage("Age must be greater than 0");
        }
    }
}
