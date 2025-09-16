namespace TrainingCenterAPI.DTOs.Courses
{
    public class PutCourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
