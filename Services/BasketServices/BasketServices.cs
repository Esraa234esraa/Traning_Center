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

        public BasketServices(ApplicationDbContext context)
        {
            _context = context;
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
                             IsVisible = c.IsVisible,
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
    }
}
