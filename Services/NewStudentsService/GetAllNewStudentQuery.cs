namespace TrainingCenterAPI.Services.NewStudentsService
{
    public class GetAllNewStudentQuery
    {
        public string? SearchWord { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
