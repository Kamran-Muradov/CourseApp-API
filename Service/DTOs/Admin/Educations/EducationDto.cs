﻿namespace Service.DTOs.Admin.Educations
{
    public class EducationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Groups { get; set; }
    }
}
