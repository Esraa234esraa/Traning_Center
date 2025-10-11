using TrainingCenterAPI.DTOs.Courses;
using TrainingCenterAPI.Models.New;

namespace TrainingCenterAPI.Services.NewsServices
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext _context;
        public NewsService(ApplicationDbContext context)
        {

            _context = context;
        }
        public async Task<ResponseModel<Guid>> AddNewsAsync(AddCoursesDto courseDto)
        {
            try
            {
                string imageUrl = string.Empty;

                if (courseDto.Image != null && courseDto.Image.Length > 0)
                {
                    // مسار حفظ الصورة
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NewsUploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(courseDto.Image.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await courseDto.Image.CopyToAsync(stream);
                    }


                    imageUrl = $"wwwroot/NewsUploads/{uniqueFileName}";
                }

                var course = new News
                {

                    Name = courseDto.Name,
                    Description = courseDto.Description,
                    FilePath = imageUrl ?? "",
                    IsActive = true,
                    IsVisible = true


                };
                _context.news.Add(course);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(course.Id, "تمت الاضافة بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت الاضافة  ");
            }

        }
        public async Task<ResponseModel<List<GetAllCoursesDto>>> GetAllNewsAsync()
        {
            var courses = await _context.news.Where(x => x.IsDeleted == false)
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
                return ResponseModel<List<GetAllCoursesDto>>.FailResponse("  لا توجد اخبار ");

            return ResponseModel<List<GetAllCoursesDto>>.SuccessResponse(courses, "news retrieved successfully");
        }

        public async Task<ResponseModel<bool>> DeleteNewsAsync(Guid id)
        {
            try
            {
                var course = await _context.news.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
                if (course == null)
                    return ResponseModel<bool>.FailResponse(" الاخبار ليست موجودة");

                // 👇 Soft delete
                course.DeletedAt = DateTime.UtcNow;
                course.IsActive = false;
                course.IsDeleted = true;
                _context.news.Update(course);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل الخبر الى سلة المهملات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الحذف ");

            }


        }


        public async Task<ResponseModel<Guid>> UpdateNewsAsync(Guid id, PutCourseDto courseDto)
        {
            try
            {
                var oldCourse = await _context.news.FirstOrDefaultAsync(c => c.Id == id);
                if (oldCourse == null)
                {
                    return ResponseModel<Guid>.FailResponse($"هذا الخبر غير موجود ");
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
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NewsUploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(courseDto.Image.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await courseDto.Image.CopyToAsync(stream);
                    }


                    imageUrl = $"wwwroot/NewsUploads/{uniqueFileName}";
                }



                oldCourse.Name = courseDto.Name;
                oldCourse.Description = courseDto.Description;
                oldCourse.FilePath = imageUrl ?? "";
                _context.news.Update(oldCourse);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(oldCourse.Id, "تمت التعديل بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت  التعديل   ");
            }
        }

        public async Task<ResponseModel<News>> GetNewsByIdAsync(Guid id)
        {
            var course = await _context.news.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (course == null)
                return ResponseModel<News>.FailResponse("الخبر  ليس موجودة");


            return ResponseModel<News>.SuccessResponse(course, "news retrieved successfully");
        }
        public async Task<ResponseModel<bool>> HideNewsAsync(Guid id)
        {
            try
            {
                var course = await _context.news.FirstOrDefaultAsync(x => x.Id == id && x.IsVisible == true);
                if (course == null)
                    return ResponseModel<bool>.FailResponse(" الخبر موجودة او ليست ظاهره ");

                // 👇 Soft delete
                course.UpdatedAt = DateTime.UtcNow;
                course.IsVisible = false;
                _context.news.Update(course);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل الخبر الى  المخفيات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الخفي ");

            }


        }
        public async Task<ResponseModel<bool>> VisibleNewsAsync(Guid id)
        {
            try
            {
                var course = await _context.news.FirstOrDefaultAsync(x => x.Id == id && x.IsVisible == false);
                if (course == null)
                    return ResponseModel<bool>.FailResponse("الدراسة ليست موجودة او ليست مخفيه");

                // 👇 Soft delete
                course.UpdatedAt = DateTime.UtcNow;
                course.IsVisible = true;
                _context.news.Update(course);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل الدراسة الى  الظهور");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الظهور ");

            }


        }
        public async Task<ResponseModel<List<GetAllCoursesDto>>> GetOnlyVisibleNewsAsync()
        {
            var courses = await _context.news.Where(x => x.IsDeleted == false && x.IsVisible == true)
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
