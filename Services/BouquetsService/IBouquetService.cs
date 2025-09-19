using TrainingCenterAPI.DTOs.Bouquets;
using TrainingCenterAPI.Models.Bouquets;

namespace TrainingCenterAPI.Services.BouquetsService
{
    public interface IBouquetService
    {
        Task<ResponseModel<List<GetAllBouquetsDTO>>> GetAllBouquets();
        Task<ResponseModel<List<GetAllBouquetsDTO>>> GetAllBouquetsOfLevel(Guid LevelId);
        Task<ResponseModel<Guid>> AddBouquet(AddBouquetDTO dTO);
        Task<ResponseModel<Guid>> UpdateBouquet(Guid Id, UpdateBouquetDTO dTO);
        Task<ResponseModel<bool>> DeleteBouquet(Guid Id);
        Task<ResponseModel<Bouquet>> GetBouquetByIdAsync(Guid id);
    }
}
