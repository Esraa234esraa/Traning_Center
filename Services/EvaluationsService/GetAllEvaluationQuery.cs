namespace TrainingCenterAPI.Services.EvaluationsService
{
    public class GetAllEvaluationQuery
    {
        public string? SearchWord { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
