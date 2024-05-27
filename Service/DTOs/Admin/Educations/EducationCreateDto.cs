using FluentValidation;

namespace Service.DTOs.Admin.Educations
{
    public class EducationCreateDto
    {
        public string Name { get; set; }
    }

    public class EducationCreateDtoValidator : AbstractValidator<EducationCreateDto>
    {
        public EducationCreateDtoValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name cannot exceed 50 characters");
        }
    }
}
