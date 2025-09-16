namespace TrainingCenterAPI.DTOs.Courses
{
    public class GetAllCoursesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
