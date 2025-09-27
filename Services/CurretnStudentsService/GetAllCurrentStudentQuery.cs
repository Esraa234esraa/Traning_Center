namespace TrainingCenterAPI.Services.CurretnStudentsService
{
    public class GetAllCurrentStudentQuery
    {
        public string? SearchWord { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
