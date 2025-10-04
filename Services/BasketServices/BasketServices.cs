using TrainingCenterAPI.DTOs.Classes;
using TrainingCenterAPI.DTOs.Courses;
using TrainingCenterAPI.DTOs.CurrentStudents;
using TrainingCenterAPI.DTOs.NewStudents;
using static TrainingCenterAPI.Enums.Enums;

namespace TrainingCenterAPI.Services.BasketServices
{
    public class BasketServices : IBasketServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public BasketServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ResponseModel<List<GetAllClassesOfBouquetDTO>>> GetAllClassesDelete()
        {
            var Levels = await _context.Classes.Include(x => x.Bouquet).AsNoTracking()
             .Where(x => x.IsDeleted == true
)

             .Select(x => new GetAllClassesOfBouquetDTO
             {
                 Id = x.Id,
                 BouquetName = x.Bouquet.BouquetName,
                 StartDate = x.StartDate,
                 EndDate = x.EndDate,
                 ClassTime = x.ClassTime,
                 BouquetId = x.BouquetId,
                 CurrentStudentsCount = x.CurrentStudentsCount,
                 Status = x.Status

             }).ToListAsync();

            if (Levels.Count() <= 0)
                return ResponseModel<List<GetAllClassesOfBouquetDTO>>.FailResponse("لا توجد حصص محذوفة ");

            return ResponseModel<List<GetAllClassesOfBouquetDTO>>.SuccessResponse(Levels, "Classes deleted retrieved successfully");
        }

        public async Task<ResponseModel<List<GetAllCoursesDto>>> GetAllCoursesAsyncDelete()
        {
            var courses = await _context.Course.Where(x => x.IsDeleted == true)
                         .OrderByDescending(x => x.CreatedAt)
                         .AsNoTracking()

                         .Select(c => new GetAllCoursesDto
                         {
                             Id = c.Id,
                             Name = c.Name,
                             Description = c.Description,
                             FilePath = c.FilePath,
                             IsActive = c.IsActive,

                             CreateAt = c.CreatedAt

                         })
                         .ToListAsync();
            if (courses.Count() <= 0)
                return ResponseModel<List<GetAllCoursesDto>>.FailResponse(" لا توجد دراسة او دورات محذوفة");

            return ResponseModel<List<GetAllCoursesDto>>.SuccessResponse(courses, "Courses deleted retrieved successfully");
        }

        public async Task<ResponseModel<List<GetAllCurrentStudentDTO>>> GetAllCurrentStudentDelete()
        {
            var Students = await _context.currents
                   .AsNoTracking()
                   .Where(x => x.IsDeleted == true)
                   .Select(x => new GetAllCurrentStudentDTO
                   {
                       Id = x.Id,
                       StudentName = x.StudentName,
                       City = x.City,
                       PhoneNumber = x.PhoneNumber,
                       IsPaid = x.IsPaid,

                       // 👇 كل الكلاسات اللي الطالب فيها
                       Classes = x.GetCurrentStudentClasses
                          .Select(cs => new ClassForStudentDTO
                          {
                              ClassId = cs.Class.Id,
                              BouquetName = cs.Class.Bouquet.BouquetName,
                              BouquetNumber = cs.Class.Bouquet.StudentsPackageCount,
                              CourseName = cs.Class.Bouquet.Course.Name,
                              LevelNumber = cs.Class.Bouquet.Level.LevelNumber,
                              LevelName = cs.Class.Bouquet.Level.Name
                          }).ToList()
                   })
                   .ToListAsync();


            if (Students.Count() <= 0)
                return ResponseModel<List<GetAllCurrentStudentDTO>>.FailResponse("لا توجد حصص محذوفة ");

            return ResponseModel<List<GetAllCurrentStudentDTO>>.SuccessResponse(Students, "Classes deleted retrieved successfully");

        }

        public async Task<ResponseModel<List<GetAllNewStudentDTO>>> GetAllNewStudentDelete()
        {

            var newStudents = await _context.newStudents.Where(x => x.status == NewStudentStatus.New && x.IsDeleted == true)
                .Select(item => new GetAllNewStudentDTO
                {

                    Id = item.Id,
                    StudentName = item.StudentName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    Gender = item.Gender,
                    City = item.City,
                    Date = item.Date,
                    Time = item.Time,
                    status = item.status,
                    CreatedAt = item.CreatedAt

                }).ToListAsync();
            if (newStudents == null || newStudents.Count() <= 0)
            {

                return ResponseModel<List<GetAllNewStudentDTO>>.FailResponse(" لاتوجد طلاب جدد محذوفة ");
            }

            return ResponseModel<List<GetAllNewStudentDTO>>.SuccessResponse(newStudents, "تم رجوع الطلاب الجدد  محذوفة بنجاح");


        }

        public async Task<ResponseModel<List<GetAllTeacherDto>>> GetAllTeachersAsyncDelete()
        {
            var Teachers = await _context.TeacherDetails.Include(x => x.User).Include(x => x.Course)


                  .Include(x => x.Classes)
                  .AsNoTracking()
                    .Where(x => x.IsDeleted == true)

                    .Select(x => new GetAllTeacherDto
                    {
                        Id = x.Id,
                        Email = x.User.Email,
                        PhoneNumber = x.User.PhoneNumber,
                        FullName = x.User.FullName,
                        CourseName = x.Course.Name,
                        City = x.City,
                        Gender = x.Gender,
                        //   AvailableClasses = 7 - (x.Classes.Count()),
                        CourseId = x.Course.Id,

                    }).ToListAsync();

            if (Teachers.Count() <= 0)
                return ResponseModel<List<GetAllTeacherDto>>.FailResponse("لا توجد معلمين محذوفة ");

            return ResponseModel<List<GetAllTeacherDto>>.SuccessResponse(Teachers, "Teachers deleted retrieved successfully");
        }

        public async Task<ResponseModel<List<GetAllNewStudentDTO>>> GetAllWaitingNewStudentDelete()
        {
            var newStudents = await _context.newStudents.Where(x => x.status == NewStudentStatus.waiting && x.IsDeleted == true)
              .Select(item => new GetAllNewStudentDTO
              {

                  Id = item.Id,
                  StudentName = item.StudentName,
                  Email = item.Email,
                  PhoneNumber = item.PhoneNumber,
                  Gender = item.Gender,
                  City = item.City,
                  Date = item.Date,
                  Time = item.Time,
                  status = item.status,
                  CreatedAt = item.CreatedAt


              }).ToListAsync();
            if (newStudents == null || newStudents.Count() <= 0)
            {

                return ResponseModel<List<GetAllNewStudentDTO>>.FailResponse(" لاتوجد طلاب في الانتظار محذوفة ");
            }

            return ResponseModel<List<GetAllNewStudentDTO>>.SuccessResponse(newStudents, "  تم رجوع الطلاب قائمة  الانتظار  بنجاح محذوفة");
        }

        //final delete teacher
        public async Task<ResponseModel<bool>> DeleteTeacherAsyncDelete(Guid teacherId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();


            try
            {
                var teacher = await _context.TeacherDetails
                    .Include(t => t.User)
                    .FirstOrDefaultAsync(t => t.Id == teacherId && t.IsDeleted == true);

                if (teacher == null)
                {

                    return ResponseModel<bool>.FailResponse("هذا المعلم غير موجود في في سلة المهملات ");
                }
                _context.TeacherDetails.Remove(teacher);
                await _userManager.DeleteAsync(teacher.User);



                await _context.SaveChangesAsync();

                await transaction.CommitAsync();


                return ResponseModel<bool>.SuccessResponse(true, "تم حذف المعلم نهائي بنجاح");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ResponseModel<bool>.FailResponse($"{ex.Message} فشل الحذف");
            }
        }

        public async Task<ResponseModel<string>> DeleteNewStudentDelete(Guid Id)
        {
            var student = await _context.newStudents.FirstOrDefaultAsync(x => x.Id == Id && x.IsDeleted == true);

            if (student == null)
            {

                return ResponseModel<string>.FailResponse("هذا الطالب غير موجود في في سلة المهملات");
            }

            _context.newStudents.Remove(student);
            await _context.SaveChangesAsync();
            return ResponseModel<string>.SuccessResponse(" تم حذف  الطالب نهائي");
        }

        public async Task<ResponseModel<string>> DeleteWaitingStudentDelete(Guid Id)
        {
            var student = await _context.newStudents.FirstOrDefaultAsync(x => x.Id == Id && x.status == NewStudentStatus.waiting && x.IsDeleted == true);
            if (student == null)
            {

                return ResponseModel<string>.FailResponse("هذا الطالب غير موجود في في سلة المهملات ");
            }

            student.IsDeleted = true;
            _context.newStudents.Update(student);
            await _context.SaveChangesAsync();
            return ResponseModel<string>.SuccessResponse(" تم نقل الطالب نهائي");
        }

        public async Task<ResponseModel<bool>> DeleteCurrentStudentDelete(Guid Id)
        {
            try
            {
                var student = await _context.currents
            .Include(s => s.GetCurrentStudentClasses) // تحميل العلاقات
             .FirstOrDefaultAsync(s => s.Id == Id);

                if (student == null)
                    return ResponseModel<bool>.FailResponse("الطالب  غير موجود في في سلة المهملات");
                // 👇 Soft delete
                student.DeletedAt = DateTime.UtcNow;
                student.IsDeleted = true;


                // Soft delete للعلاقات
                foreach (var sc in student.GetCurrentStudentClasses)
                {
                    _context.CurrentStudentClasses.Remove(sc);
                }
                _context.currents.Update(student);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم حذف الطالب نهائي ");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الحذف ");

            }
        }

        public async Task<ResponseModel<bool>> DeleteCourseAsyncDelete(Guid id)
        {
            try
            {
                var course = await _context.Course.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == true);
                if (course == null)
                    return ResponseModel<bool>.FailResponse("الدراسة غير موجود في في سلة المهملات");

                // 👇 Soft delete

                _context.Course.Remove(course);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم حذف الدراسة نهائي");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الحذف ");

            }
        }

        public async Task<ResponseModel<bool>> DeleteClassDelete(Guid Id)
        {
            try
            {
                var Class = await _context.Classes.FirstOrDefaultAsync(x => x.Id == Id && x.IsDeleted == true);
                if (Class == null)
                    return ResponseModel<bool>.FailResponse("الحصةغير موجود في في سلة المهملات");


                _context.Classes.Remove(Class);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم حذف    الحصة   نهائي");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الحذف ");

            }
        }


    }
}
