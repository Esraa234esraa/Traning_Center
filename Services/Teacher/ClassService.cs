namespace TrainingCenterAPI.Services.Implementations
{
    //public class ClassService : IClassService
    //{
    //    private readonly ApplicationDbContext _context;

    //    public ClassService(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    // ✅ 1. جلب حصة + طلابها
    //    // 1️⃣ جلب الحصة + الطلاب
    //    public async Task<ResponseModel<ClassWithStudentsDto>> GetClassWithStudentsAsync(Guid classId)
    //    {
    //        var lesson = await _context.Classes
    //            .Include(c => c.Level) // 👈 مستوى الحصة
    //            .Include(c => c.StudentClasses)
    //                .ThenInclude(sc => sc.Student)
    //                    .ThenInclude(s => s.Level) // 👈 مستوى الطالب
    //            .FirstOrDefaultAsync(c => c.Id == classId);

    //        if (lesson == null)
    //            return ResponseModel<ClassWithStudentsDto>.FailResponse("الحصة غير موجودة");

    //        var dto = new ClassWithStudentsDto
    //        {
    //            Id = lesson.Id,
    //            LevelNumber = lesson.Level.LevelNumber, // ✅
    //            LevelName = lesson.Level.Name,          // ✅
    //            PackageSize = lesson.PackageSize,
    //            CurrentStudentsCount = lesson.StudentClasses.Count,
    //            Status = lesson.Status,
    //            ClassTime = lesson.ClassTime,
    //            StartDate = lesson.StartDate,
    //            EndDate = lesson.EndDate,
    //            Students = lesson.StudentClasses.Select(sc => new StudentDto
    //            {
    //                Id = sc.Student.Id,
    //                FullName = sc.Student.FullName,
    //                Email = sc.Student.Email,
    //                PhoneNumber = sc.Student.PhoneNumber,
    //                IsPaid = sc.IsPaid,
    //                LevelNumber = sc.Student.Level.LevelNumber, // ✅
    //                LevelName = sc.Student.Level.Name           // ✅
    //            }).ToList()
    //        };

    //        return ResponseModel<ClassWithStudentsDto>.SuccessResponse(dto, "تم جلب بيانات الحصة والطلاب");
    //    }

    //    // ✅ 2. إضافة طالب للحصة
    //    public async Task<ResponseModel<StudentDto>> AddStudentToClassAsync(Guid classId, Guid studentId, bool isPaid)
    //    {
    //        var lesson = await _context.Classes
    //            .Include(c => c.StudentClasses)
    //            .FirstOrDefaultAsync(c => c.Id == classId);

    //        if (lesson == null)
    //            return ResponseModel<StudentDto>.FailResponse("الحصة غير موجودة");

    //        var student = await _context.Users.FindAsync(studentId);
    //        if (student == null)
    //            return ResponseModel<StudentDto>.FailResponse("الطالب غير موجود");

    //        // ✅ لو الحصة ممتلئة → نحجز للطالب في الانتظار
    //        if (lesson.StudentClasses.Count >= lesson.PackageSize)
    //        {
    //            var waitingEntry = new WaitingList
    //            {
    //                Id = Guid.NewGuid(),
    //                ClassId = lesson.Id,
    //                StudentId = student.Id,
    //                AddedAt = DateTime.UtcNow
    //            };

    //            _context.WaitingList.Add(waitingEntry);
    //            await _context.SaveChangesAsync();

    //            return ResponseModel<StudentDto>.SuccessResponse(new StudentDto
    //            {
    //                Id = student.Id,
    //                FullName = student.FullName,
    //                Email = student.Email,
    //                PhoneNumber = student.PhoneNumber,
    //                IsPaid = isPaid
    //            }, "الحصة مكتملة ✅ تم حجز الطالب في قائمة الانتظار");
    //        }

    //        // ✅ لو في مكان → نضيف الطالب للحصة
    //        var studentClass = new StudentClass
    //        {
    //            Id = Guid.NewGuid(),
    //            ClassId = lesson.Id,
    //            StudentId = student.Id,
    //            IsPaid = isPaid
    //        };

    //        _context.StudentClasses.Add(studentClass);
    //        await _context.SaveChangesAsync();

    //        return ResponseModel<StudentDto>.SuccessResponse(new StudentDto
    //        {
    //            Id = student.Id,
    //            FullName = student.FullName,
    //            Email = student.Email,
    //            PhoneNumber = student.PhoneNumber,
    //            IsPaid = isPaid
    //        }, "تمت إضافة الطالب للحصة");
    //    }

    //    // ✅ 3. حذف طالب من الحصة
    //    public async Task<ResponseModel<bool>> RemoveStudentFromClassAsync(Guid classId, Guid studentId)
    //    {
    //        var studentClass = await _context.StudentClasses
    //            .FirstOrDefaultAsync(sc => sc.ClassId == classId && sc.StudentId == studentId);

    //        if (studentClass == null)
    //            return ResponseModel<bool>.FailResponse("الطالب غير موجود في هذه الحصة");

    //        _context.StudentClasses.Remove(studentClass);
    //        await _context.SaveChangesAsync();

    //        return ResponseModel<bool>.SuccessResponse(true, "تم حذف الطالب من الحصة");
    //    }

    //    // ✅ 4. ترقية طالب من قائمة الانتظار
    //    public async Task<ResponseModel<StudentDto>> PromoteStudentFromWaitingListAsync(Guid classId)
    //    {
    //        var waitingEntry = await _context.WaitingList
    //            .Include(w => w.Student) // هنا Student هو ApplicationUser
    //            .Where(w => w.ClassId == classId)
    //            .OrderBy(w => w.AddedAt)
    //            .FirstOrDefaultAsync();

    //        if (waitingEntry == null)
    //            return ResponseModel<StudentDto>.FailResponse("لا يوجد طلاب في قائمة الانتظار");

    //        var lesson = await _context.Classes
    //            .Include(c => c.StudentClasses)
    //            .FirstOrDefaultAsync(c => c.Id == classId);

    //        if (lesson == null)
    //            return ResponseModel<StudentDto>.FailResponse("الحصة غير موجودة");

    //        if (lesson.StudentClasses.Count >= lesson.PackageSize)
    //            return ResponseModel<StudentDto>.FailResponse("الحصة ما زالت مكتملة");

    //        var studentClass = new StudentClass
    //        {
    //            Id = Guid.NewGuid(),
    //            ClassId = lesson.Id,
    //            StudentId = waitingEntry.StudentId,
    //            IsPaid = false
    //        };

    //        _context.StudentClasses.Add(studentClass);
    //        _context.WaitingList.Remove(waitingEntry);

    //        await _context.SaveChangesAsync();

    //        var dto = new StudentDto
    //        {
    //            Id = waitingEntry.Student.Id,
    //            FullName = waitingEntry.Student.FullName,      // ✅ صح
    //            Email = waitingEntry.Student.Email,            // ✅ صح
    //            PhoneNumber = waitingEntry.Student.PhoneNumber,// ✅ صح
    //            IsPaid = false
    //        };

    //        return ResponseModel<StudentDto>.SuccessResponse(dto, "تم نقل الطالب من الانتظار إلى الحصة");
    //    }
    //    public async Task<ResponseModel<StudentDto>> UpdateStudentInClassAsync(Guid classId, Guid studentId, bool isPaid)
    //    {
    //        var studentClass = await _context.StudentClasses
    //            .Include(sc => sc.Student)
    //            .FirstOrDefaultAsync(sc => sc.ClassId == classId && sc.StudentId == studentId);

    //        if (studentClass == null)
    //            return ResponseModel<StudentDto>.FailResponse("الطالب غير موجود في هذه الحصة");

    //        // تحديث حالة الدفع
    //        studentClass.IsPaid = isPaid;
    //        await _context.SaveChangesAsync();

    //        var dto = new StudentDto
    //        {
    //            Id = studentClass.Student.Id,
    //            FullName = studentClass.Student.FullName,
    //            Email = studentClass.Student.Email,
    //            PhoneNumber = studentClass.Student.PhoneNumber,
    //            IsPaid = studentClass.IsPaid
    //        };

    //        return ResponseModel<StudentDto>.SuccessResponse(dto, "تم تحديث بيانات الطالب في الحصة");
    //    }
    //    public async Task<ResponseModel<List<AllClassesForTeacherDto>>> GetAllClassesByTeacherId(Guid teacherId)
    //    {
    //        var classes = await _context.Classes
    //            .Where(x => x.TeacherId == teacherId && x.Status == ClassStatus.Active)
    //            .Select(x => new AllClassesForTeacherDto
    //            {
    //                Id = x.Id,
    //                PackageSize = x.PackageSize,

    //                Status = x.Status,
    //                StartDate = x.StartDate,
    //                EndDate = x.EndDate,

    //            }).ToListAsync();
    //        return ResponseModel<List<AllClassesForTeacherDto>>.SuccessResponse(classes, "تمت رجوع الحصص الخاصة بالمعلم بنجاح ");
    //    }
    //}
}
