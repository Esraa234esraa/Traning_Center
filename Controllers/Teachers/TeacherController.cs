using Microsoft.AspNetCore.Mvc;
using TrainingCenterAPI.DTOs.Teacher;
using TrainingCenterAPI.Responses;
using TrainingCenterAPI.Services.Teacher;

namespace TrainingCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // ✅ 1. جلب كل المعلمين
        [HttpGet("all")]
        public async Task<ActionResult<ResponseModel<List<TeacherWithClassesDto>>>> GetAllTeachers()
        {
            return await _teacherService.GetAllTeachersAsync();
        }

        // ✅ 2. جلب معلم واحد
        [HttpGet("{teacherId}")]
        public async Task<ActionResult<ResponseModel<TeacherWithClassesDto>>> GetTeacherById(Guid teacherId)
        {
            return await _teacherService.GetTeacherByIdAsync(teacherId);
        }

        // ✅ 3. إضافة معلم جديد
        [HttpPost("add")]
        public async Task<IActionResult> AddTeacher([FromBody] TeacherCreateRequest request)
        {
            if (request == null || request.Teacher == null)
                return BadRequest(ResponseModel<TeacherDto>.FailResponse("الطلب غير صالح"));

            request.Teacher.DeletedAt = null; // DeletedAt افتراضي null

            var response = await _teacherService.AddTeacherAsync(request.Teacher, request.Password);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TeacherLoginRequest request)
        {
            var response = await _teacherService.LoginTeacherAsync(request.Email, request.Password);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }


        // ✅ 4. تعديل بيانات معلم
        [HttpPut("{teacherId}")]
        public async Task<ActionResult<ResponseModel<TeacherDto>>> UpdateTeacher(Guid teacherId, [FromBody] TeacherDto teacherDto)
        {
            return await _teacherService.UpdateTeacherAsync(teacherId, teacherDto);
        }

        // ✅ 5. حذف معلم
        [HttpDelete("{teacherId}")]
        public async Task<ActionResult<ResponseModel<bool>>> DeleteTeacher(Guid teacherId)
        {
            return await _teacherService.DeleteTeacherAsync(teacherId);
        }

        // ✅ 6. جلب معلم + حصصه
        [HttpGet("{teacherId}/classes")]
        public async Task<ActionResult<ResponseModel<TeacherWithClassesDto>>> GetTeacherWithClasses(Guid teacherId)
        {
            return await _teacherService.GetTeacherWithClassesAsync(teacherId);
        }

        // ✅ 7. جلب كل الطلاب لكل حصص المعلم
        [HttpGet("{teacherId}/students")]
        public async Task<ActionResult<ResponseModel<AllStudentsForTeacherDto>>> GetAllStudentsByTeacher(Guid teacherId)
        {
            return await _teacherService.GetAllStudentsByTeacherIdAsync(teacherId);
        }
    }

  
}
