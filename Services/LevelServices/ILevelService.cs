namespace TrainingCenterAPI.Services
{
    public interface ILevelService
    {
        Task<ResponseModel<List<GetAllLevelsDTO>>> GetAllLevelsOfCourse(Guid CourseId);
        Task<ResponseModel<Guid>> AddLevel(AddLevelDTO dTO);
        Task<ResponseModel<Guid>> UpdateLevel(Guid Id, UpdateLevelDTO dTO);
        Task<ResponseModel<bool>> DeleteLevel(Guid Id);
    }
}
