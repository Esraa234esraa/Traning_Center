using TrainingCenterAPI.DTOs.CurrentStudents;

namespace TrainingCenterAPI.Services.CurretnStudentsService
{
    public interface ICurrentStudentService
    {
        Task<ResponseModel<Guid>> AddCurrentStudent(AddCurrentStudentDTO dTO);

    }
}
