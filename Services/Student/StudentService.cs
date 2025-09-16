namespace TrainingCenterAPI.Services.Student
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public StudentService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;

        }
        //public async Task<ResponseModel<PostStudentDTO>> AddStudent(PostStudentDTO DTO)
        //{

        //    using var transaction = await _context.Database.BeginTransactionAsync();
        //    try
        //    {

        //        var user = new ApplicationUser
        //        {
        //            Id = Guid.NewGuid(),
        //            UserName = DTO.Email,
        //            Email = DTO.Email,

        //            FullName = DTO.FullName,
        //            PhoneNumber = DTO.PhoneNumber,
        //            Role = "Student",
        //            CreatedAt = DateTime.UtcNow,
        //            IsActive = true
        //        };

        //        var result = await _userManager.CreateAsync(user, "abcde@fgh1234");
        //        if (!result.Succeeded)
        //        {
        //            return ResponseModel<PostStudentDTO>.FailResponse("فشل إنشاء طالب: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        //        }

        //        var studentDetails = new StudentDetails
        //        {
        //            TeacherId = DTO.TeacherId,
        //            UserId = user.Id,
        //            City = DTO.City,
        //            CourseName = DTO.CourseName,
        //            studentStatus = StudentStatus.Accepted,

        //        };
        //        _context.studentDetails.Add(studentDetails);

        //        var studentClass = new StudentClass()
        //        {


        //            StudentId = user.Id,
        //            ClassId = DTO.ClassId,
        //            IsPaid = DTO.IsPaid,
        //            LevelId = DTO.LevelNumber,

        //        };
        //        _context.StudentClasses.Add(studentClass);

        //        _context.SaveChanges();
        //        await transaction.CommitAsync();

        //        return ResponseModel<PostStudentDTO>.SuccessResponse(DTO, "تمت إضافة المعلم بنجاح مع 7 حصص تلقائيًا");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Rollback on any error
        //        await transaction.RollbackAsync();
        //        return ResponseModel<PostStudentDTO>.FailResponse("فشل العملية: " + ex.Message);
        //    }

        //}

        //public async Task<ResponseModel<List<GetCurrentStudentDTO>>> GetCurrentStudentStudent()
        //{
        //    var dto = await _context.Users.Include(x => x.StudentDetails).

        //        Include(x => x.Classes).

        //      Where(x => x.Role == "Student" &&
        //      x.StudentDetails.FirstOrDefault().
        //      studentStatus == StudentStatus.Accepted).


        //        Select(item => new GetCurrentStudentDTO
        //        {
        //            FullName = item.FullName,
        //            PhoneNumber = item.PhoneNumber,
        //            CourseName = item.StudentDetails.FirstOrDefault().CourseName ?? "",
        //            TeacherName = item.FullName,
        //            IsPaid = item.Classes.FirstOrDefault().IsPaid,
        //            PackageSize = item.Classes.FirstOrDefault().Class.PackageSize,

        //        }
        //    ).ToListAsync();
        //    return ResponseModel<List<GetCurrentStudentDTO>>.SuccessResponse(dto, "تمتا");
        //}


    }
}
