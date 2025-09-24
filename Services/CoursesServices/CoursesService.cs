using TrainingCenterAPI.DTOs.Courses;
using TrainingCenterAPI.Models.Courses;

namespace TrainingCenterAPI.Services.CoursesServices
{
    public class CoursesService : ICoursesServices
    {
        private readonly ApplicationDbContext _context;
        public CoursesService(ApplicationDbContext context)
        {

            _context = context;
        }
        public async Task<ResponseModel<Guid>> AddCourseAsync(AddCoursesDto courseDto)
        {
            try
            {
                string imageUrl = string.Empty;

                if (courseDto.Image != null && courseDto.Image.Length > 0)
                {
                    // مسار حفظ الصورة
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(courseDto.Image.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await courseDto.Image.CopyToAsync(stream);
                    }


                    imageUrl = $"wwwroot/uploads/{uniqueFileName}";
                }

                var course = new Course
                {

                    Name = courseDto.Name,
                    Description = courseDto.Description,
                    FilePath = imageUrl ?? "",
                    IsActive = true,
                    IsVisible = true

                };
                _context.Course.Add(course);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(course.Id, "تمت الاضافة بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت الاضافة  ");
            }

        }
        public async Task<ResponseModel<List<GetAllCoursesDto>>> GetAllCoursesAsync()
        {
            var courses = await _context.Course.Where(x => x.IsDeleted == false)
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
                return ResponseModel<List<GetAllCoursesDto>>.FailResponse("لا توجد دراسة او دورات");

            return ResponseModel<List<GetAllCoursesDto>>.SuccessResponse(courses, "Courses retrieved successfully");
        }
        public async Task<ResponseModel<List<GetAllCoursesDto>>> GetOnlyVisibleCoursesAsync()
        {
            var courses = await _context.Course.Where(x => x.IsDeleted == false && x.IsVisible == true)
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
                return ResponseModel<List<GetAllCoursesDto>>.FailResponse("لا توجد دراسة او دورات");

            return ResponseModel<List<GetAllCoursesDto>>.SuccessResponse(courses, "Courses retrieved successfully");
        }
        public async Task<ResponseModel<bool>> DeleteCourseAsync(Guid id)
        {
            try
            {
                var course = await _context.Course.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
                if (course == null)
                    return ResponseModel<bool>.FailResponse("الدراسة ليست موجودة");

                // 👇 Soft delete
                course.DeletedAt = DateTime.UtcNow;
                course.IsActive = false;
                course.IsDeleted = true;
                _context.Course.Update(course);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل الدراسة الى سلة المهملات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الحذف ");

            }


        }
        public async Task<ResponseModel<bool>> HideCourseAsync(Guid id)
        {
            try
            {
                var course = await _context.Course.FirstOrDefaultAsync(x => x.Id == id && x.IsVisible == true);
                if (course == null)
                    return ResponseModel<bool>.FailResponse("الدراسة ليست موجودة او ليست ظاهره ");

                // 👇 Soft delete
                course.UpdatedAt = DateTime.UtcNow;
                course.IsVisible = false;
                _context.Course.Update(course);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل الدراسة الى  المخفيات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الخفي ");

            }


        }
        public async Task<ResponseModel<bool>> VisibleCourseAsync(Guid id)
        {
            try
            {
                var course = await _context.Course.FirstOrDefaultAsync(x => x.Id == id && x.IsVisible == false);
                if (course == null)
                    return ResponseModel<bool>.FailResponse("الدراسة ليست موجودة او ليست مخفيه");

                // 👇 Soft delete
                course.UpdatedAt = DateTime.UtcNow;
                course.IsVisible = true;
                _context.Course.Update(course);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل الدراسة الى  الظهور");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الظهور ");

            }


        }

        public async Task<ResponseModel<Guid>> UpdateCourseAsync(Guid id, PutCourseDto courseDto)
        {
            try
            {
                var oldCourse = await _context.Course.FirstOrDefaultAsync(c => c.Id == id);
                if (oldCourse == null)
                {
                    return ResponseModel<Guid>.FailResponse($"هذا الكورس غير موجود ");
                }


                string imageUrl = string.Empty;

                if (courseDto.Image != null && courseDto.Image.Length > 0)
                {
                    // نحذف الصورة القديمة أولاً
                    if (!string.IsNullOrEmpty(oldCourse.FilePath)) // course.OldImagePath = الرابط المخزن في DB
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), oldCourse.FilePath);

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }





                    // مسار حفظ الصورة
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(courseDto.Image.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await courseDto.Image.CopyToAsync(stream);
                    }


                    imageUrl = $"wwwroot/uploads/{uniqueFileName}";
                }



                oldCourse.Name = courseDto.Name;
                oldCourse.Description = courseDto.Description;
                oldCourse.FilePath = imageUrl ?? "";
                _context.Course.Update(oldCourse);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(oldCourse.Id, "تمت التعديل بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت  التعديل   ");
            }
        }

        public async Task<ResponseModel<Course>> GetCourseByIdAsync(Guid id)
        {
            var course = await _context.Course.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (course == null)
                return ResponseModel<Course>.FailResponse("الدراسة ليست موجودة");


            return ResponseModel<Course>.SuccessResponse(course, "Courses retrieved successfully");
        }
    }
}


