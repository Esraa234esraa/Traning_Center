using TrainingCenterAPI.DTOs.Levels;

namespace TrainingCenterAPI.Services.Level
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
        public Task<ResponseModel<Guid>> AddLevel(AddLevelDTO dTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<GetAllLevelsDTO>>> GetAllLevels()
        {
            throw new NotImplementedException();
        }
    }
}
