using TrainingCenterAPI.DTOs.Courses;
using TrainingCenterAPI.Models.ExternalCourses;

namespace TrainingCenterAPI.Services.ExternalCoursesServices
{
    public class ExternalCoursesServices : IExternalCoursesServices
    {
        private readonly ApplicationDbContext _context;
        public ExternalCoursesServices(ApplicationDbContext context)
        {

            _context = context;
        }
        public async Task<ResponseModel<Guid>> AddExternalCourseAsync(AddCoursesDto courseDto)
        {
            try
            {
                string imageUrl = string.Empty;

                if (courseDto.Image != null && courseDto.Image.Length > 0)
                {
                    // مسار حفظ الصورة
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ExternalUploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(courseDto.Image.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await courseDto.Image.CopyToAsync(stream);
                    }


                    imageUrl = $"wwwroot/ExternalUploads/{uniqueFileName}";
                }

                var course = new ExternalCourse
                {

                    Name = courseDto.Name,
                    Description = courseDto.Description,
                    FilePath = imageUrl ?? "",
                    IsActive = true,
                    IsVisible = true


                };
                _context.ExternalCourses.Add(course);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(course.Id, "تمت الاضافة بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت الاضافة  ");
            }

        }
        public async Task<ResponseModel<List<GetAllCoursesDto>>> GetAllExternalCoursesAsync()
        {
            var courses = await _context.ExternalCourses.Where(x => x.IsDeleted == false)
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
                return ResponseModel<List<GetAllCoursesDto>>.FailResponse("  لا توجد دراسة او دورات خارجية");

            return ResponseModel<List<GetAllCoursesDto>>.SuccessResponse(courses, "Courses retrieved successfully");
        }

        public async Task<ResponseModel<bool>> DeleteExternalCourseAsync(Guid id)
        {
            try
            {
                var course = await _context.ExternalCourses.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
                if (course == null)
                    return ResponseModel<bool>.FailResponse("الدراسة الخارجية ليست موجودة");

                // 👇 Soft delete
                course.DeletedAt = DateTime.UtcNow;
                course.IsActive = false;
                course.IsDeleted = true;
                _context.ExternalCourses.Update(course);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل الدراسة الى سلة المهملات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الحذف ");

            }


        }


        public async Task<ResponseModel<Guid>> UpdateExternalCourseAsync(Guid id, PutCourseDto courseDto)
        {
            try
            {
                var oldCourse = await _context.ExternalCourses.FirstOrDefaultAsync(c => c.Id == id);
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
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ExternalUploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(courseDto.Image.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await courseDto.Image.CopyToAsync(stream);
                    }


                    imageUrl = $"wwwroot/ExternalUploads/{uniqueFileName}";
                }



                oldCourse.Name = courseDto.Name;
                oldCourse.Description = courseDto.Description;
                oldCourse.FilePath = imageUrl ?? "";
                _context.ExternalCourses.Update(oldCourse);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(oldCourse.Id, "تمت التعديل بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت  التعديل   ");
            }
        }

        public async Task<ResponseModel<ExternalCourse>> GetExternalCourseByIdAsync(Guid id)
        {
            var course = await _context.ExternalCourses.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (course == null)
                return ResponseModel<ExternalCourse>.FailResponse("الدراسة الخارجية ليست موجودة");


            return ResponseModel<ExternalCourse>.SuccessResponse(course, "Courses retrieved successfully");
        }
        public async Task<ResponseModel<bool>> HideExternalCourseAsync(Guid id)
        {
            try
            {
                var course = await _context.ExternalCourses.FirstOrDefaultAsync(x => x.Id == id && x.IsVisible == true);
                if (course == null)
                    return ResponseModel<bool>.FailResponse("الدراسة ليست موجودة او ليست ظاهره ");

                // 👇 Soft delete
                course.UpdatedAt = DateTime.UtcNow;
                course.IsVisible = false;
                _context.ExternalCourses.Update(course);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل الدراسة الى  المخفيات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الخفي ");

            }


        }
        public async Task<ResponseModel<bool>> VisibleExternalCourseAsync(Guid id)
        {
            try
            {
                var course = await _context.ExternalCourses.FirstOrDefaultAsync(x => x.Id == id && x.IsVisible == false);
                if (course == null)
                    return ResponseModel<bool>.FailResponse("الدراسة ليست موجودة او ليست مخفيه");

                // 👇 Soft delete
                course.UpdatedAt = DateTime.UtcNow;
                course.IsVisible = true;
                _context.ExternalCourses.Update(course);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل الدراسة الى  الظهور");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الظهور ");

            }


        }
        public async Task<ResponseModel<List<GetAllCoursesDto>>> GetOnlyVisibleExternalCoursesAsync()
        {
            var courses = await _context.ExternalCourses.Where(x => x.IsDeleted == false && x.IsVisible == true)
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


    }
}
