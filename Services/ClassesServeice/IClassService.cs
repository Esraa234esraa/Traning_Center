using TrainingCenterAPI.DTOs.Classes;

namespace TrainingCenterAPI.Services.ClassesServeice
{
    public interface IClassService
    {
        Task<ResponseModel<ClassWithStudentsDto>> GetClassWithStudentsAsync(Guid classId);

        Task<ResponseModel<StudentDto>> AddStudentToClassAsync(Guid classId, Guid studentId, bool isPaid);
        Task<ResponseModel<Guid>> AddClassAsync(AddClassDTO DTO);  //new
        Task<ResponseModel<Classes>> GetClassByIdAsync(Guid id);
        Task<ResponseModel<Guid>> UpdateClassAsync(Guid Id, UpdateClassDTO DTO);
        Task<ResponseModel<bool>> DeleteClass(Guid Id);

        Task<ResponseModel<List<GetAllClassesOfBouquetDTO>>> GetAllClassesOfBouquet(Guid BouquetId);
        Task<ResponseModel<List<GetAllClassesOfBouquetDTO>>> GetOnlyAvailableClassesOfBouquet(Guid BouquetId);

        Task<ResponseModel<List<GetAllClassesOfBouquetDTO>>> GetAllClasses();
        Task<ResponseModel<List<GetAllClassesOfBouquetDTO>>> GetAllClassesEmptyFromTeacher();

        Task<ResponseModel<StudentDto>> UpdateStudentInClassAsync(Guid classId, Guid studentId, bool isPaid);
        Task<ResponseModel<bool>> RemoveStudentFromClassAsync(Guid classId, Guid studentId);
        Task<ResponseModel<List<AllClassesForTeacherDto>>> GetAllClassesByTeacherId(Guid teacherId);
    }

}
