namespace TrainingCenterAPI.Responses
{
    public class ResponseDTO
    {
        public dynamic? Result { get; set; }
        public int pageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}
