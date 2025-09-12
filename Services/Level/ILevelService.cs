using TrainingCenterAPI.DTOs.Levels;

namespace TrainingCenterAPI.Services.Level
{
    public interface ILevelService
    {
        Task<ResponseModel<List<GetAllLevelsDTO>>> GetAllLevels();
        Task<ResponseModel<Guid>> AddLevel(AddLevelDTO dTO);
    }
}
