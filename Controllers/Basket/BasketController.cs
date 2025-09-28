using TrainingCenterAPI.Services.BasketServices;

namespace TrainingCenterAPI.Controllers.Basket
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketServices _basketServices;
        public BasketController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }
        [HttpGet("GetAllCoursesDelete")]
        public async Task<IActionResult> GetAllCoursesDelete()
        {
            var result = await _basketServices.GetAllCoursesAsyncDelete();
            return Ok(result);

        }
        [HttpGet("GetAllClassesDelete")]
        public async Task<IActionResult> GetAllClassesDelete()
        {
            var result = await _basketServices.GetAllClassesDelete();
            return Ok(result);
        }
        [HttpGet("GetAllNewStudentDelete")]
        public async Task<IActionResult> GetAllNewStudentDelete()
        {

            return Ok(await _basketServices.GetAllNewStudentDelete());
        }


        [HttpGet("GetAllWaitingNewStudentDelete")]
        public async Task<IActionResult> GetAllWaitingNewStudentDelete()
        {

            return Ok(await _basketServices.GetAllWaitingNewStudentDelete());
        }

        [HttpGet("GetAllCurrentStudentDelete")]
        public async Task<IActionResult> GetAllCurrentStudentDelete()
        {
            var result = await _basketServices.GetAllCurrentStudentDelete();


            return Ok(result);
        }
        [HttpGet("GetAllTeachersAsyncDelete")]
        public async Task<IActionResult> GetAllTeachersAsyncDelete()
        {

            var response = await _basketServices.GetAllTeachersAsyncDelete();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpDelete("DeleteNewStudentDelete/{Id}")]
        public async Task<IActionResult> DeleteNewStudentDelete(Guid Id)
        {

            return Ok(await _basketServices.DeleteNewStudentDelete(Id));
        }


        [HttpDelete("DeleteWaitingStudentDelete/{Id}")]
        public async Task<IActionResult> DeleteWaitingStudentDelete(Guid Id)
        {

            return Ok(await _basketServices.DeleteWaitingStudentDelete(Id));
        }

        [HttpDelete("DeleteCurrentStudentDelete/{Id}")]
        public async Task<IActionResult> DeleteCurrentStudentDelete(Guid Id)
        {
            var result = await _basketServices.DeleteCurrentStudentDelete(Id);


            return Ok(result);
        }

        [HttpDelete("DeleteTeacherDelete/{teacherId}")]
        public async Task<ActionResult<ResponseModel<bool>>> DeleteTeacherDelete(Guid teacherId)
        {
            return await _basketServices.DeleteTeacherAsyncDelete(teacherId);
        }

        [HttpDelete("DeleteCourseDelete{id}")]
        public async Task<IActionResult> DeleteCourseDelete(Guid id)
        {
            var result = await _basketServices.DeleteCourseAsyncDelete(id);

            return Ok(result);

        }

        [HttpDelete("DeleteClassDelete/{Id}")]
        public async Task<IActionResult> DeleteClassDelete(Guid Id)
        {
            var result = await _basketServices.DeleteClassDelete(Id);
            return Ok(result);
        }

    }
}
