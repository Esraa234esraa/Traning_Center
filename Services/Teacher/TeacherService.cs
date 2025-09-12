

namespace TrainingCenterAPI.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public TeacherService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;

        }

        // ✅ 1. جلب كل المعلمين
        //public async Task<ResponseModel<List<TeacherDto>>> GetAllTeachersAsync()
        //{
        //    var teachers = await _context.TeacherDetails
        //        .Include(t => t.User)
        //        .Include(t => t.Classes) // 👈 عشان نجيب الحصص
        //            .ThenInclude(c => c.StudentClasses)
        //        .ToListAsync();

        //    var result = teachers.Select(t => new TeacherDto
        //    {
        //        Id = t.Id,
        //        FullName = t.User.FullName,
        //        Email = t.User.Email,
        //        PhoneNumber = t.User.PhoneNumber,
        //        City = t.City,
        //        CourseName = t.CourseName,

        //        AvailableClasses = t.Classes.Count(c =>
        //        c.Status == ClassStatus.Active &&   // ✅
        //        c.StudentClasses.Count < c.PackageSize)           
        //        }).ToList();

        //    return ResponseModel<List<TeacherDto>>.SuccessResponse(result, "تم جلب كل المعلمين");
        //}
        public async Task<ResponseModel<List<TeacherWithClassesDto>>> GetAllTeachersAsync()
        {
            var teachers = await _context.TeacherDetails
                .Include(t => t.User)
                .Include(t => t.Classes)
                    .ThenInclude(c => c.StudentClasses)
                .ToListAsync();

            var result = teachers.Select(t => new TeacherWithClassesDto
            {
                Id = t.Id,
                FullName = t.User.FullName,
                Email = t.User.Email,
                PhoneNumber = t.User.PhoneNumber,
                City = t.City,
                CourseName = t.CourseName,
                DeletedAt = t.DeletedAt,

                // 🟢 الحصص الخاصة بالمعلم
                Classes = t.Classes.Select(c => new ClassDto
                {
                    Id = c.Id,
                    LevelNumber = c.Level.LevelNumber,
                    LevelName = c.Level.Name,
                    PackageSize = c.PackageSize,
                    CurrentStudentsCount = c.StudentClasses.Count,
                    Status = c.Status,
                    ClassTime = c.ClassTime,
                    DeletedAt = c.DeletedAt,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate
                }).ToList()
            }).ToList();

            return ResponseModel<List<TeacherWithClassesDto>>.SuccessResponse(result, "تم جلب كل المعلمين");
        }

        // ✅ 2. جلب معلم واحد
        public async Task<ResponseModel<TeacherWithClassesDto>> GetTeacherByIdAsync(Guid teacherId)
        {
            var teacher = await _context.TeacherDetails
                .Include(t => t.User)
                .Include(t => t.Classes)
                    .ThenInclude(c => c.StudentClasses)
                .FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher == null)
                return ResponseModel<TeacherWithClassesDto>.FailResponse("المعلم غير موجود");

            var dto = new TeacherWithClassesDto
            {
                Id = teacher.Id,
                FullName = teacher.User.FullName,
                Email = teacher.User.Email,
                PhoneNumber = teacher.User.PhoneNumber,
                City = teacher.City,
                CourseName = teacher.CourseName,
                DeletedAt = teacher.DeletedAt,

                Classes = teacher.Classes.Select(c => new ClassDto
                {
                    Id = c.Id,
                    LevelNumber = c.Level.LevelNumber,
                    LevelName = c.Level?.Name ?? string.Empty,
                    PackageSize = c.PackageSize,
                    CurrentStudentsCount = c.StudentClasses.Count,
                    Status = c.Status,
                    ClassTime = c.ClassTime,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    DeletedAt = c.DeletedAt
                }).ToList()
            };

            return ResponseModel<TeacherWithClassesDto>.SuccessResponse(dto, "تم جلب بيانات المعلم بنجاح");
        }

        // ✅ 3. إضافة معلم جديد
        public async Task<ResponseModel<TeacherDto>> AddTeacherAsync(TeacherDto teacherDto, string password)
        {
            // 1️⃣ إنشاء المستخدم
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = teacherDto.Email,
                Email = teacherDto.Email,
                FullName = teacherDto.FullName,
                PhoneNumber = teacherDto.PhoneNumber,
                Role = "Teacher",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return ResponseModel<TeacherDto>.FailResponse("فشل إنشاء المعلم: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // 2️⃣ إنشاء بيانات المعلم
            var teacher = new TeacherDetails
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                City = teacherDto.City,
                CourseName = teacherDto.CourseName,
                Classes = new List<Classes>()
            };

            // إنشاء 7 حصص تلقائي لكل معلم
            for (int i = 1; i <= 7; i++)
            {
                teacher.Classes.Add(new Classes
                {
                    Id = Guid.NewGuid(),           // يتولد تلقائي
                    Status = ClassStatus.Active,   // أو الحالة الأولية
                    CurrentStudentsCount = 0,
                    LevelId = Guid.Empty           // نسيبه فاضي وسيتم تحديده عند إضافة أول طالب
                });
            }


            // 4️⃣ حفظ في قاعدة البيانات
            _context.TeacherDetails.Add(teacher);
            await _context.SaveChangesAsync();

            // 5️⃣ إعادة DTO بدون Id من العميل
            teacherDto.Id = teacher.Id;
            teacherDto.AvailableClasses = 7; // ✅ لأنه لسه كل الحصص فاضية
            return ResponseModel<TeacherDto>.SuccessResponse(teacherDto, "تمت إضافة المعلم بنجاح مع 7 حصص تلقائيًا");
        }

        // ✅ 4. تعديل بيانات معلم
        public async Task<ResponseModel<TeacherDto>> UpdateTeacherAsync(Guid teacherId, TeacherDto teacherDto)
        {
            var teacher = await _context.TeacherDetails
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher == null)
                return ResponseModel<TeacherDto>.FailResponse("المعلم غير موجود");

            teacher.User.FullName = teacherDto.FullName;
            teacher.User.Email = teacherDto.Email;
            teacher.User.PhoneNumber = teacherDto.PhoneNumber;
            teacher.City = teacherDto.City;
            teacher.CourseName = teacherDto.CourseName;

            _context.TeacherDetails.Update(teacher);
            await _context.SaveChangesAsync();

            return ResponseModel<TeacherDto>.SuccessResponse(teacherDto, "تم تحديث بيانات المعلم");
        }

        // ✅ 5. حذف معلم
        public async Task<ResponseModel<bool>> DeleteTeacherAsync(Guid teacherId)
        {
            var teacher = await _context.TeacherDetails
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher == null)
                return ResponseModel<bool>.FailResponse("المعلم غير موجود");

            // نحذف أولاً الـ User من الهوية
            await _userManager.DeleteAsync(teacher.User);

            _context.TeacherDetails.Remove(teacher);
            await _context.SaveChangesAsync();

            return ResponseModel<bool>.SuccessResponse(true, "تم حذف المعلم بنجاح");
        }

        // ✅ 6. معلم + حصصه
        public async Task<ResponseModel<TeacherWithClassesDto>> GetTeacherWithClassesAsync(Guid teacherId)
        {
            var teacher = await _context.TeacherDetails
                .Include(t => t.User)
                .Include(t => t.Classes)
                .Include(t => t.Classes)
    .ThenInclude(c => c.Level)

                .FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher == null)
                return ResponseModel<TeacherWithClassesDto>.FailResponse("المعلم غير موجود");

            var dto = new TeacherWithClassesDto
            {
                Id = teacher.Id,
                FullName = teacher.User.FullName,
                Email = teacher.User.Email,
                PhoneNumber = teacher.User.PhoneNumber,
                City = teacher.City,
                CourseName = teacher.CourseName,
                Classes = teacher.Classes.Select(c => new ClassDto
                {
                    Id = c.Id,
                    LevelNumber = c.Level.LevelNumber,    // 👈 الرقم من جدول Level
                    LevelName = c.Level.Name,             // 👈 الاسم لو انت ضايفه في جدول Level
                    PackageSize = c.PackageSize,
                    CurrentStudentsCount = c.StudentClasses.Count,
                    Status = c.Status,
                    ClassTime = c.ClassTime,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate
                }).ToList()


            };

            return ResponseModel<TeacherWithClassesDto>.SuccessResponse(dto, "تم جلب بيانات المعلم وحصصه");
        }

        // ✅ 7. كل الطلاب لكل حصص المعلم
        public async Task<ResponseModel<AllStudentsForTeacherDto>> GetAllStudentsByTeacherIdAsync(Guid teacherId)
        {
            var teacher = await _context.TeacherDetails
                .Include(t => t.User)
                .Include(t => t.Classes)
                    .ThenInclude(c => c.StudentClasses)
                        .ThenInclude(sc => sc.Student)  // ✅ مهم عشان نجيب بيانات الطالب
                .FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher == null)
                return ResponseModel<AllStudentsForTeacherDto>.FailResponse("المعلم غير موجود");

            var students = teacher.Classes
                .SelectMany(c => c.StudentClasses)
                .Select(sc => new StudentDto
                {
                    Id = sc.Student.Id,                   // ✅ من Student مش StudentClass
                    FullName = sc.Student.FullName,       // ✅ اسم الطالب
                    Email = sc.Student.Email,
                    PhoneNumber = sc.Student.PhoneNumber,
                    IsPaid = sc.IsPaid                    // حالة الدفع من جدول الربط
                })
                .DistinctBy(s => s.Id) // ✅ لو نفس الطالب في أكتر من حصة، يجيب نسخة واحدة فقط
                .ToList();

            var dto = new AllStudentsForTeacherDto
            {
                TeacherId = teacher.Id,
                TeacherName = teacher.User.FullName,
                Students = students
            };

            return ResponseModel<AllStudentsForTeacherDto>.SuccessResponse(dto, "تم جلب كل طلاب المعلم");
        }
        public async Task<ResponseModel<TeacherLoginResponse>> LoginTeacherAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return ResponseModel<TeacherLoginResponse>.FailResponse("البريد الإلكتروني غير موجود");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
                return ResponseModel<TeacherLoginResponse>.FailResponse("كلمة المرور غير صحيحة");

            // إنشاء JWT
            var token = GenerateJwtToken(user);

            var response = new TeacherLoginResponse
            {
                Token = token,
                FullName = user.FullName,
                Id = user.Id
            };

            return ResponseModel<TeacherLoginResponse>.SuccessResponse(response, "تم تسجيل الدخول بنجاح");
        }
        private string GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim("id", user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
