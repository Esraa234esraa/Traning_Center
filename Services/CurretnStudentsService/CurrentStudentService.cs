using TrainingCenterAPI.DTOs.CurrentStudents;
using TrainingCenterAPI.Models.Students;
using static TrainingCenterAPI.Enums.Enums;

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
                if (dto.Id != null)
                {
                    var waiting = _context.newStudents.FirstOrDefault(x => x.Id == dto.Id && x.status == NewStudentStatus.waiting);
                    if (waiting != null)
                    {
                        waiting.IsDeleted = true;
                        _context.newStudents.Update(waiting);
                    }


                }
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

        public async Task<ResponseModel<List<GetAllCurrentStudentDTO>>> GetAllCurrentStudent()
        {
            var Levels = await _context.currents.AsNoTracking()
                .Include(x => x.GetCurrentStudentClasses).ThenInclude(x => x.Class)
                .ThenInclude(x => x.Bouquet).ThenInclude(x => x.Level).ThenInclude(x => x.Course)
                .Where(x => x.IsDeleted == false).

                Select(x => new GetAllCurrentStudentDTO
                {
                    Id = x.Id,
                    StudentName = x.StudentName,
                    City = x.City,
                    PhoneNumber = x.PhoneNumber,
                    BouquetName = x.GetCurrentStudentClasses.FirstOrDefault().Class.Bouquet.BouquetName,
                    BouquetNumber = x.GetCurrentStudentClasses.FirstOrDefault().Class.Bouquet.StudentsPackageCount,
                    CourseName = x.GetCurrentStudentClasses.FirstOrDefault().Class.Bouquet.Course.Name


                }).ToListAsync();

            if (Levels.Count() <= 0)
                return ResponseModel<List<GetAllCurrentStudentDTO>>.FailResponse("لا توجد حصص اضيفتة ");

            return ResponseModel<List<GetAllCurrentStudentDTO>>.SuccessResponse(Levels, "Classes retrieved successfully");
        }
        public async Task<ResponseModel<Guid>> UpdateCurrentStudent(Guid Id, UpdateCurrentStudentDTO dTO)
        {

            try
            {
                var oldCurrent = await _context.currents
                    .Include(s => s.GetCurrentStudentClasses)

                    .FirstOrDefaultAsync(c => c.Id == Id && c.IsDeleted == false);
                if (oldCurrent == null)
                {
                    return ResponseModel<Guid>.FailResponse($"هذا الطالب غير موجود ");
                }







                oldCurrent.StudentName = dTO.StudentName;
                oldCurrent.Email = dTO.Email;
                oldCurrent.PhoneNumber = dTO.PhoneNumber;
                oldCurrent.Gender = dTO.Gender;
                oldCurrent.City = dTO.City;
                oldCurrent.IsPaid = dTO.IsPaid;



                // تحديث الصفوف المرتبطة
                oldCurrent.GetCurrentStudentClasses.Clear(); // مسح القديم

                oldCurrent.GetCurrentStudentClasses.Add(new CurrentStudentClass
                {
                    ClassId = dTO.ClassId,
                    StudentId = oldCurrent.Id
                });

                _context.currents.Update(oldCurrent);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(oldCurrent.Id, "تمت التعديل بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت  التعديل   ");
            }
        }
        public async Task<ResponseModel<bool>> DeleteCurrentStudent(Guid Id)
        {
            try
            {
                var student = await _context.currents
        .Include(s => s.GetCurrentStudentClasses) // تحميل العلاقات
             .FirstOrDefaultAsync(s => s.Id == Id);

                if (student == null)
                    return ResponseModel<bool>.FailResponse("الطالب غير موجود");
                // 👇 Soft delete
                student.DeletedAt = DateTime.UtcNow;
                student.IsDeleted = true;


                // Soft delete للعلاقات
                foreach (var sc in student.GetCurrentStudentClasses)
                {
                    sc.IsDeleted = true;
                }
                _context.currents.Update(student);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل الطالب الى سلة المهملات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الحذف ");

            }
        }
    }
}
