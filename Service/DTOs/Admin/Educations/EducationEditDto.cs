using FluentValidation;

namespace Service.DTOs.Admin.Educations
{
    public class EducationEditDto
    {
        public string Name { get; set; }
    }

    public class EducationEditDtoValidator : AbstractValidator<EducationEditDto>
    {
        public EducationEditDtoValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name cannot exceed 50 characters");
        }
    }
}
