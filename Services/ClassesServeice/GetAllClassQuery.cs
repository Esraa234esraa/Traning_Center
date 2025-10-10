namespace TrainingCenterAPI.Services.ClassesServeice
{
    public class GetAllClassQuery
    {
        public string? SearchWord { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
