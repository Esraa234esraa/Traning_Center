using TrainingCenterAPI.DTOs.Teacher.CLassesToTeacher;
using TrainingCenterAPI.DTOs.Teacher.ViewMyClasses;
using TrainingCenterAPI.Services.Teacher;

namespace TrainingCenterAPI.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public EmailService email;

        public TeacherService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, EmailService _email)
        {
            _context = context;
            _userManager = userManager;
            email = _email;


        }


        public async Task<ResponseModel<Guid>> AddTeacherAsync(AddTeacherDto teacherDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {



                // 1️⃣ إنشاء المستخدم
                var user = new ApplicationUser
                {
                    UserName = teacherDto.Email,
                    Email = teacherDto.Email,
                    FullName = teacherDto.FullName,
                    PhoneNumber = teacherDto.PhoneNumber,
                    Role = "Teacher",

                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user, teacherDto.Password);
                if (!result.Succeeded)
                {
                    return ResponseModel<Guid>.FailResponse("فشل إنشاء المعلم: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                // 2️⃣ إنشاء بيانات المعلم
                var teacher = new TeacherDetails
                {

                    UserId = user.Id,
                    City = teacherDto.City,
                    Gender = teacherDto.Gender,
                    CourseId = teacherDto.CourseId,
                    Classes = new List<Classes>()
                };


                //string opt = email.GenerateOtp();
                //email.SendVerificationEmailAsync(teacher.User.Email, opt);



                // 4️⃣ حفظ في قاعدة البيانات
                _context.TeacherDetails.Add(teacher);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();


                await email.SendEmailAsync(
      teacher.User.Email,
    "دوام",
    $"مرحبا بيك في مركز اللغة المثاليه للتدريب لقد تم اضافتك للعمل معنا، نتمنى لك التوفيق والنجاح.{Environment.NewLine}{Environment.NewLine}" +
    $"اسم المستخدم وكلمة السر هما:{Environment.NewLine}" +
    $"UserName: {teacher.User.Email}{Environment.NewLine}" +
    $"Password: {teacherDto.Password}{Environment.NewLine}{Environment.NewLine}" +
    $"⚠️ اسم المستخدم وكلمة السر سري للغاية"
);

                return ResponseModel<Guid>.SuccessResponse(user.Id, "تمت إضافة المعلم بنجاح");
            }

            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ResponseModel<Guid>.FailResponse($"{ex.Message} الطلب غير صالح");
            }
        }


        public async Task<ResponseModel<Guid>> AddClassToTeacherAsync(AddClassTeacherDto Dto)
        {

            try
            {

                var teacher = await _context.TeacherDetails.FirstOrDefaultAsync(x => x.Id == Dto.TeacherId);
                if (teacher == null)
                    return ResponseModel<Guid>.FailResponse($" الطلب غير صالح");

                var Class = await _context.Classes.Include(x => x.Teacher).Include(x => x.Bouquet)

                    .ThenInclude(x => x.Level).ThenInclude(x => x.Course)

                    .FirstOrDefaultAsync(x => x.Id == Dto.ClassId);
                if (Class == null)
                    return ResponseModel<Guid>.FailResponse($"هذه الحصة غير موجودة");

                if (Class.Teacher != null)
                    return ResponseModel<Guid>.FailResponse($"هذه الحصة مضاف لها معلم");

                if (Class.Bouquet.Level.Course.Id != teacher.CourseId)

                    return ResponseModel<Guid>.FailResponse($"هذه الحصة لا تخص كورس المعلم");

                Class.TeacherId = Dto.TeacherId;

                _context.Classes.Update(Class);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(Class.Id, "تمت إضافة معلم الي حصة بنجاح");
            }

            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message} الطلب غير صالح");
            }
        }

        public async Task<ResponseModel<List<GetAllTeacherDto>>> GetAllTeachersAsync()
        {
            var Teachers = await _context.TeacherDetails.Include(x => x.User).Include(x => x.Course)


                .Include(x => x.Classes)
                .AsNoTracking()
                  .Where(x => x.IsDeleted == false)

                  .Select(x => new GetAllTeacherDto
                  {
                      UserId = x.UserId,
                      Id = x.Id,
                      Email = x.User.Email,
                      PhoneNumber = x.User.PhoneNumber,
                      FullName = x.User.FullName,
                      CourseName = x.Course.Name,
                      City = x.City,
                      Gender = x.Gender,
                      AvailableClasses = 7 - (x.Classes.Count()),
                      CourseId = x.Course.Id,

                  }).ToListAsync();

            if (Teachers.Count() <= 0)
                return ResponseModel<List<GetAllTeacherDto>>.FailResponse("لا توجد معلمين اضيفت ");

            return ResponseModel<List<GetAllTeacherDto>>.SuccessResponse(Teachers, "Teachers retrieved successfully");
        }

        public async Task<ResponseModel<Guid>> ResetPassword(Guid UserId, string Password)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user == null)
                return ResponseModel<Guid>.FailResponse("المعلم غير موجود");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, Password);
            if (!result.Succeeded)
            {

                return ResponseModel<Guid>.FailResponse("فشل تحديث  كلمة السر : " + string.Join(", ", result.Errors.Select(e => e.Description)));

            }
            await email.SendEmailAsync(
user.Email,
"دوام",
$" لقد تم  تغير كلمة المرور نتمنى لك التوفيق والنجاح.{Environment.NewLine}{Environment.NewLine}" +
$"  كلمة السر هي:{Environment.NewLine}" +

$"Password: {Password}{Environment.NewLine}{Environment.NewLine}" +
$"⚠️ اسم المستخدم وكلمة السر سري للغاية"
);

            return ResponseModel<Guid>.SuccessResponse(UserId, "تم تحديث  كلمة السر بنجاح");
        }


        // ✅ 4. تعديل بيانات معلم
        public async Task<ResponseModel<Guid>> UpdateTeacherAsync(Guid teacherId, UpdateTeacherDto teacherDto)
        {
            var teacher = await _context.TeacherDetails
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher == null)
                return ResponseModel<Guid>.FailResponse("المعلم غير موجود");





            teacher.User.FullName = teacherDto.FullName;
            teacher.User.Email = teacherDto.Email;
            teacher.User.PhoneNumber = teacherDto.PhoneNumber;
            teacher.User.Password = teacherDto.Password;
            teacher.City = teacherDto.City;
            teacher.CourseId = teacherDto.CourseId;
            //   teacher.CourseName = teacherDto.CourseName;

            _context.TeacherDetails.Update(teacher);
            await _context.SaveChangesAsync();

            return ResponseModel<Guid>.SuccessResponse(teacher.Id, "تم تحديث بيانات المعلم");
        }

        // ✅ 5. حذف معلم
        public async Task<ResponseModel<bool>> DeleteTeacherAsync(Guid teacherId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();


            try
            {
                var teacher = await _context.TeacherDetails
                    .Include(t => t.User)
                    .FirstOrDefaultAsync(t => t.Id == teacherId);

                if (teacher == null)
                    return ResponseModel<bool>.FailResponse("المعلم غير موجود");
                teacher.IsDeleted = true;
                _context.TeacherDetails.Update(teacher);
                await _userManager.UpdateAsync(teacher.User);


                // نحذف أولاً الـ User من الهوية



                await _context.SaveChangesAsync();

                await transaction.CommitAsync();


                return ResponseModel<bool>.SuccessResponse(true, "تم حذف المعلم بنجاح");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ResponseModel<bool>.FailResponse($"{ex.Message} فشل الحذف");
            }
        }



        //   ✅ 6. بورفايل المعلم: معلم + حصصه
        public async Task<ResponseModel<TeacherViewDTO>> GetProfileTeacherWithClassesAsync(Guid teacherId)
        {
            try
            {
                var teacher = await _context.TeacherDetails.Include(x => x.User)
                .Where(t => t.User.Id == teacherId && t.IsDeleted != true)
                 .Select(t => new TeacherViewDTO
                 {
                     Id = t.Id,
                     CourseName = t.Course.Name,

                     Classes = t.Classes.Select(c => new TeacherClassDtoView
                     {
                         Id = c.Id,
                         LevelNumber = c.Bouquet.Level.LevelNumber,
                         LevelName = c.Bouquet.Level.Name ?? "not add name",
                         PackageSize = c.Bouquet.StudentsPackageCount,
                         CurrentStudentsCount = c.GetCurrentStudentClasses.Count(),
                         Status = c.Status,
                         ClassTime = c.ClassTime,
                         StartDate = c.StartDate,
                         EndDate = c.EndDate,

                         Students = c.GetCurrentStudentClasses
               .Select(cs => new CurrentStudentForTeacherDTO
               {
                   // 👈 مش StudentName
                   FullName = cs.Student.StudentName,

               }).ToList()
                     }).ToList()
                 })
                   .FirstOrDefaultAsync();
                if (teacher == null)
                    return ResponseModel<TeacherViewDTO>.FailResponse("هذ المعلم غير موجود");

                return ResponseModel<TeacherViewDTO>.SuccessResponse(teacher, "تم جلب بيانات المعلم وحصصه");
            }
            catch (Exception ex)
            {
                return ResponseModel<TeacherViewDTO>.FailResponse($"{ex.Message} حدث خطاء ");
            }


        }
        public async Task<ResponseModel<TeacherByIdDTO>> GetTeacherById(Guid teacherId)
        {
            try
            {
                var teacher = await _context.TeacherDetails
                .Where(t => t.Id == teacherId && t.IsDeleted != true)
                 .Select(t => new TeacherByIdDTO
                 {
                     Id = t.Id,
                     FullName = t.User.FullName,
                     Email = t.User.Email,
                     City = t.City,
                     PhoneNumber = t.User.PhoneNumber,
                     Password = t.User.Password,
                     CourseName = t.Course.Name,
                     CourseId = t.CourseId
                 })


                   .FirstOrDefaultAsync();
                if (teacher == null)
                    return ResponseModel<TeacherByIdDTO>.FailResponse("هذ المعلم غير موجود");

                return ResponseModel<TeacherByIdDTO>.SuccessResponse(teacher, "تم جلب بيانات المعلم ");
            }
            catch (Exception ex)
            {
                return ResponseModel<TeacherByIdDTO>.FailResponse($"{ex.Message} حدث خطاء ");
            }


        }

        public async Task<ResponseModel<TeacherViewDTO>> GetProfileTeacherWithClassesAsyncByAdmin(Guid teacherId)
        {
            try
            {
                var teacher = await _context.TeacherDetails.Include(x => x.User)
                .Where(t => t.User.Id == teacherId && t.IsDeleted != true)
                 .Select(t => new TeacherViewDTO
                 {
                     Id = t.Id,
                     CourseName = t.Course.Name,

                     Classes = t.Classes.Select(c => new TeacherClassDtoView
                     {
                         Id = c.Id,
                         LevelNumber = c.Bouquet.Level.LevelNumber,
                         LevelName = c.Bouquet.Level.Name ?? "not add name",
                         PackageSize = c.Bouquet.StudentsPackageCount,
                         CurrentStudentsCount = c.GetCurrentStudentClasses.Count(),
                         Status = c.Status,
                         ClassTime = c.ClassTime,
                         StartDate = c.StartDate,
                         EndDate = c.EndDate,

                         Students = c.GetCurrentStudentClasses
               .Select(cs => new CurrentStudentForTeacherDTO
               {
                   // 👈 مش StudentName
                   StudentId = cs.Student.Id,
                   FullName = cs.Student.StudentName,

               }).ToList()
                     }).ToList()
                 })
                   .FirstOrDefaultAsync();
                if (teacher == null)
                    return ResponseModel<TeacherViewDTO>.FailResponse("هذ المعلم غير موجود");

                return ResponseModel<TeacherViewDTO>.SuccessResponse(teacher, "تم جلب بيانات المعلم وحصصه");
            }
            catch (Exception ex)
            {
                return ResponseModel<TeacherViewDTO>.FailResponse($"{ex.Message} حدث خطاء ");
            }

        }
    }

}



