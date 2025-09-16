using TrainingCenterAPI.DTOs.Levels;

namespace TrainingCenterAPI.Services
{
    public class LevelService : ILevelService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public LevelService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;

        }
        public async Task<ResponseModel<Guid>> AddLevel(AddLevelDTO dTO)
        {
            try
            {
                var Level = new Level
                {

                    Name = dTO.Name,
                    LevelNumber = dTO.LevelNumber,
                    CourseId = dTO.CourseId,

                };
                _context.levels.Add(Level);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(Level.Id, "تمت الاضافة بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت الاضافة  ");
            }

        }

        public async Task<ResponseModel<bool>> DeleteLevel(Guid Id)
        {
            try
            {
                var level = await _context.levels.FirstOrDefaultAsync(x => x.Id == Id && x.IsDeleted == false);
                if (level == null)
                    return ResponseModel<bool>.FailResponse("المستوى ليس موجودة");

                // 👇 Soft delete
                level.DeletedAt = DateTime.UtcNow;
                level.IsDeleted = true;
                _context.levels.Update(level);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل المستوى الى سلة المهملات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الحذف ");

            }
        }

        public async Task<ResponseModel<List<GetAllLevelsDTO>>> GetAllLevelsOfCourse(Guid CourseId)
        {
            var Levels = await _context.levels.Include(x => x.Course).AsNoTracking()
                .Where(x => x.CourseId == CourseId && x.IsDeleted == false)

                .Select(x => new GetAllLevelsDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    LevelNumber = x.LevelNumber,
                    CourseName = x.Course.Name,

                }).ToListAsync();

            if (Levels.Count() <= 0)
                return ResponseModel<List<GetAllLevelsDTO>>.FailResponse("لا توجد مستويات اضيفت لهذا الكورس ");

            return ResponseModel<List<GetAllLevelsDTO>>.SuccessResponse(Levels, "Levels retrieved successfully");
        }

        public async Task<ResponseModel<Guid>> UpdateLevel(Guid Id, UpdateLevelDTO dTO)
        {

            try
            {
                var oldLevel = await _context.levels.FirstOrDefaultAsync(c => c.Id == Id);
                if (oldLevel == null)
                {
                    return ResponseModel<Guid>.FailResponse($"هذا المستوى غير موجود ");
                }







                oldLevel.Name = dTO.Name;
                oldLevel.LevelNumber = dTO.LevelNumber;
                oldLevel.CourseId = dTO.CourseId;
                _context.levels.Update(oldLevel);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(oldLevel.Id, "تمت التعديل بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت  التعديل   ");
            }
        }
    }
}
