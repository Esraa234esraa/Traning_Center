namespace TrainingCenterAPI.Services.CurretnStudentsService
{
    public class GetAllCurrentStudentQuery
    {
        public string? SearchWord { get; set; }
        public Paid IsPaid { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public enum Paid
    {
        Paid = 1,
        NotPaid = 2


    }
}
