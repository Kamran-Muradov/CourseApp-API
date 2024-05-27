﻿using FluentValidation;
using Service.DTOs.Admin.Students;

namespace Service.DTOs.Admin.Teachers
{
    public class TeacherCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public List<int> GroupIds { get; set; }
    }

    public class TeacherCreateDtoValidator : AbstractValidator<TeacherCreateDto>
    {
        public TeacherCreateDtoValidator()
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


            RuleFor(m => m.Salary)
                .NotEmpty()
                .WithMessage("Salary is required")
                .GreaterThan(0)
                .WithMessage("Salary must be greater than 0");

            RuleFor(m => m.GroupIds)
                .NotEmpty()
                .WithMessage("At least one group is required");
        }
    }
}
