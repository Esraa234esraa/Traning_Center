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
                    TeacherId = x.Teacher != null ? x.Teacher!.UserId : null,
                    BouquetName = x.Bouquet.BouquetName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    ClassTime = x.ClassTime,
                    BouquetId = x.BouquetId,
                    CurrentStudentsCount = x.GetCurrentStudentClasses.Count(),
                    BouquetCount = x.Bouquet.StudentsPackageCount,
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
                    return ResponseModel<bool>.FailResponse("الحصة ليس موجودة");

                // 👇 Soft delete
                Class.DeletedAt = DateTime.UtcNow;
                Class.IsDeleted = true;
                _context.Classes.Update(Class);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل    الحصة الى سلة المهملات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الحذف ");

            }
        }




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










