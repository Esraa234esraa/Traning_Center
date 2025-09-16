namespace TrainingCenterAPI.DTOs.Courses
{
    public class AddCoursesDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

    }
}
