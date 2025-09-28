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

        [HttpGet("GetProfileTeacherWithClassesAsyncByAdmin/{teacherId}")]
        public async Task<ActionResult<ResponseModel<TeacherViewDTO>>> GetProfileTeacherWithClassesAsyncByAdmin(Guid teacherId)
        {
            return await _teacherService.GetProfileTeacherWithClassesAsyncByAdmin(teacherId);
        }
        [HttpGet("GetTeacherById/{teacherId}")]
        public async Task<ActionResult<ResponseModel<TeacherByIdDTO>>> GetTeacherById(Guid teacherId)
        {
            return await _teacherService.GetTeacherById(teacherId);
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

        [HttpPut("ResetPassword/{teacherId}")]
        public async Task<ActionResult<ResponseModel<Guid>>> ResetPassword(Guid teacherId, string password)
        {
            return await _teacherService.ResetPassword(teacherId, password);
        }






    }


}
