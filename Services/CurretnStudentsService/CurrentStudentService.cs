using TrainingCenterAPI.DTOs.CurrentStudents;
using TrainingCenterAPI.Models.Students;

namespace TrainingCenterAPI.Services.CurretnStudentsService
{
    public class CurrentStudentService : ICurrentStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public CurrentStudentService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;

        }

        public async Task<ResponseModel<Guid>> AddCurrentStudent(AddCurrentStudentDTO dto)
        {
            try
            {
                var student = new CurrentStudent
                {
                    StudentName = dto.StudentName,
                    Gender = dto.Gender,
                    City = dto.City,
                    PhoneNumber = dto.PhoneNumber,
                    IsPaid = dto.IsPaid
                };

                // link only one class
                student.GetCurrentStudentClasses.Add(new CurrentStudentClass
                {
                    ClassId = dto.ClassId,
                    Student = student
                });

                _context.currents.Add(student);
                await _context.SaveChangesAsync();
                return ResponseModel<Guid>.SuccessResponse(student.Id, "تمت الاضافة بنجاح");

            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت الاضافة  ");
            }
        }
    }
}
