using TrainingCenterAPI.DTOs.Bouquets;
using TrainingCenterAPI.Models.Bouquets;

namespace TrainingCenterAPI.Services.BouquetsService
{
    public class BouquetService : IBouquetService
    {
        private readonly ApplicationDbContext _context;
        public BouquetService(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<ResponseModel<Guid>> AddBouquet(AddBouquetDTO dTO)
        {
            try
            {
                var bouquet = new Bouquet
                {

                    BouquetName = dTO.BouquetName,
                    CourseId = dTO.CourseId,
                    LevelId = dTO.LevelId,
                    Money = dTO.Money,
                    StudentsPackageCount = dTO.StudentsPackageCount,

                };
                _context.bouquets.Add(bouquet);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(bouquet.Id, "تمت الاضافة بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت الاضافة  ");
            }

        }

        public async Task<ResponseModel<bool>> DeleteBouquet(Guid Id)
        {
            try
            {
                var bouquet = await _context.bouquets.FirstOrDefaultAsync(x => x.Id == Id && x.IsDeleted == false);
                if (bouquet == null)
                    return ResponseModel<bool>.FailResponse("الباقة ليست موجودة");

                // 👇 Soft delete
                bouquet.DeletedAt = DateTime.UtcNow;
                bouquet.IsDeleted = true;
                _context.bouquets.Update(bouquet);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل الباقة الى سلة المهملات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الحذف ");

            }
        }

        public async Task<ResponseModel<List<GetAllBouquetsDTO>>> GetAllBouquets()
        {
            var bouquets = await _context.bouquets
                .Where(x => x.IsDeleted == false)
                 .AsNoTracking()
                .Include(x => x.Course)
                .ThenInclude(x => x.Levels)


                .Select(x => new GetAllBouquetsDTO
                {
                    Id = x.Id,
                    BouquetName = x.BouquetName,
                    StudentsPackageCount = x.StudentsPackageCount,
                    Money = x.Money,
                    LevelName = x.Level.Name,
                    CourseName = x.Level.Course.Name,
                    LevelNumber = x.Level.LevelNumber,

                }).ToListAsync();

            if (bouquets.Count() <= 0)
                return ResponseModel<List<GetAllBouquetsDTO>>.FailResponse("لا توجد باقات ");

            return ResponseModel<List<GetAllBouquetsDTO>>.SuccessResponse(bouquets, "تم استرجاع الباقات بنجاح");
        }

        public async Task<ResponseModel<List<GetAllBouquetsDTO>>> GetAllBouquetsOfLevel(Guid LevelId)
        {
            var bouquets = await _context.bouquets
                 .Where(x => x.LevelId == LevelId && x.IsDeleted == false)
                .Include(x => x.Course)

                .ThenInclude(x => x.Levels)

                .AsNoTracking()


               .Select(x => new GetAllBouquetsDTO
               {
                   Id = x.Id,
                   BouquetName = x.BouquetName,
                   StudentsPackageCount = x.StudentsPackageCount,
                   Money = x.Money,
                   LevelName = x.Level.Name,
                   CourseName = x.Level.Course.Name,
                   LevelNumber = x.Level.LevelNumber,

               }).ToListAsync();

            if (bouquets.Count() <= 0)
                return ResponseModel<List<GetAllBouquetsDTO>>.FailResponse("  لا توجد باقات  لهذا المستوى ");

            return ResponseModel<List<GetAllBouquetsDTO>>.SuccessResponse(bouquets, "تم استرجاع الباقات بنجاح");
        }

        public async Task<ResponseModel<Guid>> UpdateBouquet(Guid Id, UpdateBouquetDTO dTO)
        {
            try
            {
                var oldBouquet = await _context.bouquets.FirstOrDefaultAsync(c => c.Id == Id && c.IsDeleted == false);
                if (oldBouquet == null)
                {
                    return ResponseModel<Guid>.FailResponse($"هذه الباقة غير موجودة ");
                }







                oldBouquet.CourseId = dTO.CourseId;
                oldBouquet.LevelId = dTO.LevelId;
                oldBouquet.Money = dTO.Money;
                oldBouquet.BouquetName = dTO.BouquetName;
                oldBouquet.StudentsPackageCount = dTO.StudentsPackageCount;
                _context.bouquets.Update(oldBouquet);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(oldBouquet.Id, "تمت التعديل بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت  التعديل   ");
            }
        }
    }
}

