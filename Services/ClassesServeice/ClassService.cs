using TrainingCenterAPI.DTOs.Classes;

namespace TrainingCenterAPI.Services.ClassesServeice
{
    public class ClassService : IClassService
    {
        private readonly ApplicationDbContext _context;

        public ClassService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<List<GetAllClassesOfBouquetDTO>>> GetOnlyAvailableClassesOfBouquet(Guid BouquetId)
        {
            var Levels = await _context.Classes.Include(x => x.Bouquet).AsNoTracking()
                .Where(x => x.BouquetId == BouquetId && x.IsDeleted == false
                && x.EndDate > DateTime.UtcNow && (x.Status != ClassStatus.Cancelled || x.Status != ClassStatus.Completed))

                .Select(x => new GetAllClassesOfBouquetDTO
                {
                    Id = x.Id,
                    BouquetName = x.Bouquet.BouquetName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    ClassTime = x.ClassTime,
                    BouquetId = x.BouquetId,
                    CurrentStudentsCount = x.CurrentStudentsCount

                }).ToListAsync();

            if (Levels.Count() <= 0)
                return ResponseModel<List<GetAllClassesOfBouquetDTO>>.FailResponse("لا توجد حصص متاحه حاليا ");

            return ResponseModel<List<GetAllClassesOfBouquetDTO>>.SuccessResponse(Levels, "Classes retrieved successfully");
        }
        public async Task<ResponseModel<List<GetAllClassesOfBouquetDTO>>> GetAllClassesOfBouquet(Guid BouquetId)
        {
            var Levels = await _context.Classes.Include(x => x.Bouquet).AsNoTracking()
                .Where(x => x.BouquetId == BouquetId && x.IsDeleted == false)

                .Select(x => new GetAllClassesOfBouquetDTO
                {
                    Id = x.Id,
                    BouquetName = x.Bouquet.BouquetName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    ClassTime = x.ClassTime,
                    BouquetId = x.BouquetId,
                    CurrentStudentsCount = x.CurrentStudentsCount

                }).ToListAsync();

            if (Levels.Count() <= 0)
                return ResponseModel<List<GetAllClassesOfBouquetDTO>>.FailResponse("لا توجد حصص اضيفت لهذا الباقة ");

            return ResponseModel<List<GetAllClassesOfBouquetDTO>>.SuccessResponse(Levels, "Classes retrieved successfully");
        }
        public async Task<ResponseModel<List<GetAllClassesOfBouquetDTO>>> GetAllClasses()
        {
            var Levels = await _context.Classes.Include(x => x.Bouquet).AsNoTracking()
                .Where(x => x.IsDeleted == false
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
                return ResponseModel<List<GetAllClassesOfBouquetDTO>>.FailResponse("لا توجد حصص اضيفتة ");

            return ResponseModel<List<GetAllClassesOfBouquetDTO>>.SuccessResponse(Levels, "Classes retrieved successfully");
        }
        public async Task<ResponseModel<List<GetAllClassesOfBouquetDTO>>> GetAllClassesEmptyFromTeacher()
        {
            var Levels = await _context.Classes.Include(x => x.Bouquet).Include(x => x.Teacher).AsNoTracking()
                .Where(x => x.IsDeleted == false && x.Teacher == null
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
                return ResponseModel<List<GetAllClassesOfBouquetDTO>>.FailResponse("لا توجد حصص اضيفتة ");

            return ResponseModel<List<GetAllClassesOfBouquetDTO>>.SuccessResponse(Levels, "Classes retrieved successfully");
        }

        public async Task<ResponseModel<Classes>> GetClassByIdAsync(Guid id)
        {
            var Class = await _context.Classes.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (Class == null)
                return ResponseModel<Classes>.FailResponse("الدراسة ليست موجودة");


            return ResponseModel<Classes>.SuccessResponse(Class, "Courses retrieved successfully");
        }
        public async Task<ResponseModel<Guid>> AddClassAsync(AddClassDTO dTO)
        {
            try
            {
                //if (dTO.TeacherId != null)
                //{
                //    var teach = _context.Users.FirstOrDefault(x => x.Id == dTO.TeacherId);
                //    return ResponseModel<Guid>.FailResponse($"teacher Not Found  ");
                //}
                //var teacah = _context.Users.FirstOrDefault(x => x.Teacher == dTO.TeacherId);  



                var Level = new Classes
                {
                    // TeacherId = dTO.TeacherId,
                    BouquetId = dTO.BouquetId,
                    StartDate = dTO.StartDate,
                    EndDate = dTO.EndDate,
                    ClassTime = dTO.ClassTime,

                };
                _context.Classes.Add(Level);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(Level.Id, "تمت الاضافة بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت الاضافة  ");
            }

        }
        public async Task<ResponseModel<Guid>> UpdateClassAsync(Guid Id, UpdateClassDTO dTO)
        {
            try
            {
                var oldClass = await _context.Classes.FirstOrDefaultAsync(c => c.Id == Id && c.IsDeleted == false);
                if (oldClass == null)
                {
                    return ResponseModel<Guid>.FailResponse($"هذا الحصة غير موجود ");
                }




                oldClass.StartDate = dTO.StartDate;
                oldClass.EndDate = dTO.EndDate;
                oldClass.ClassTime = dTO.ClassTime;
                oldClass.BouquetId = dTO.BouquetId;
                _context.Classes.Update(oldClass);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(oldClass.Id, "تمت التعديل بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت  التعديل   ");
            }
        }

        public async Task<ResponseModel<bool>> DeleteClass(Guid Id)
        {
            try
            {
                var Class = await _context.Classes.FirstOrDefaultAsync(x => x.Id == Id && x.IsDeleted == false);
                if (Class == null)
                    return ResponseModel<bool>.FailResponse("المستوى ليس موجودة");

                // 👇 Soft delete
                Class.DeletedAt = DateTime.UtcNow;
                Class.IsDeleted = true;
                _context.Classes.Update(Class);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل المستوى الى سلة المهملات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الحذف ");

            }
        }


        //olddddddddddddddddddddddddddddddddddddddddd

        #region old

        public Task<ResponseModel<StudentDto>> AddStudentToClassAsync(Guid classId, Guid studentId, bool isPaid)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<AllClassesForTeacherDto>>> GetAllClassesByTeacherId(Guid teacherId)
        {
            throw new NotImplementedException();
        }


        public Task<ResponseModel<ClassWithStudentsDto>> GetClassWithStudentsAsync(Guid classId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> RemoveStudentFromClassAsync(Guid classId, Guid studentId)
        {
            throw new NotImplementedException();
        }


        public Task<ResponseModel<StudentDto>> UpdateStudentInClassAsync(Guid classId, Guid studentId, bool isPaid)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}






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


