//using Microsoft.AspNetCore.Mvc;
//using TrainingCenterAPI.DTOs.Teacher;
//using TrainingCenterAPI.Responses;
//using TrainingCenterAPI.Services.Teacher;

using TrainingCenterAPI.DTOs.Teacher.CLassesToTeacher;
using TrainingCenterAPI.DTOs.Teacher.ViewMyClasses;
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
        [HttpPost("add")]
        public async Task<IActionResult> AddTeacher([FromBody] AddTeacherDto request)
        {


            var response = await _teacherService.AddTeacherAsync(request);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("AddClassToTeacher")]
        public async Task<IActionResult> AddClassToTeacher([FromBody] AddClassTeacherDto request)
        {


            var response = await _teacherService.AddClassToTeacherAsync(request);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }



        [HttpPut("UpdateTeacher/{teacherId}")]
        public async Task<ActionResult<ResponseModel<Guid>>> UpdateTeacher(Guid teacherId, [FromBody] UpdateTeacherDto teacherDto)
        {
            return await _teacherService.UpdateTeacherAsync(teacherId, teacherDto);
        }



        [HttpGet("GetProfileTeacherWithClasses/{teacherId}")]
        public async Task<ActionResult<ResponseModel<TeacherViewDTO>>> GetProfileTeacherWithClasses(Guid teacherId)
        {
            return await _teacherService.GetProfileTeacherWithClassesAsync(teacherId);
        }

        [HttpGet("GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers()
        {

            var response = await _teacherService.GetAllTeachersAsync();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{teacherId}")]
        public async Task<ActionResult<ResponseModel<bool>>> DeleteTeacher(Guid teacherId)
        {
            return await _teacherService.DeleteTeacherAsync(teacherId);
        }



        //        // ✅ 1. جلب كل المعلمين
        //        [HttpGet("all")]
        //        public async Task<ActionResult<ResponseModel<List<TeacherWithClassesDto>>>> GetAllTeachers()
        //        {
        //            return await _teacherService.GetAllTeachersAsync();
        //        }

        //        // ✅ 2. جلب معلم واحد
        //        [HttpGet("{teacherId}")]
        //        public async Task<ActionResult<ResponseModel<TeacherWithClassesDto>>> GetTeacherById(Guid teacherId)
        //        {
        //            return await _teacherService.GetTeacherByIdAsync(teacherId);
        //        }

        // ✅ 3. إضافة معلم جديد



        //        [HttpPost("login")]
        //        public async Task<IActionResult> Login([FromBody] TeacherLoginRequest request)
        //        {
        //            var response = await _teacherService.LoginTeacherAsync(request.Email, request.Password);
        //            if (!response.Success)
        //                return BadRequest(response);

        //            return Ok(response);
        //        }


        //        // ✅ 4. تعديل بيانات معلم
        //        [HttpPut("{teacherId}")]






        //        // ✅ 7. جلب كل الطلاب لكل حصص المعلم
        //        [HttpGet("{teacherId}/students")]
        //        public async Task<ActionResult<ResponseModel<AllStudentsForTeacherDto>>> GetAllStudentsByTeacher(Guid teacherId)
        //        {
        //            return await _teacherService.GetAllStudentsByTeacherIdAsync(teacherId);
        //        }
    }


}
