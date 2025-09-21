using TrainingCenterAPI.DTOs.Classes;
using TrainingCenterAPI.Services.ClassesServeice;

namespace TrainingCenterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        //// ✅ 1. جلب حصة + طلابها
        //[HttpGet("{classId}/students")]
        //public async Task<IActionResult> GetClassWithStudents(Guid classId)
        //{
        //    var result = await _classService.GetClassWithStudentsAsync(classId);
        //    return Ok(result);
        //}
        [HttpGet("GetOnlyAvailableClassesOfBouquet")]
        public async Task<IActionResult> GetOnlyAvailableClassesOfBouquet(Guid BouquetId)
        {
            var result = await _classService.GetOnlyAvailableClassesOfBouquet(BouquetId);
            return Ok(result);
        }

        [HttpGet("AllClassesOfBouquet")]
        public async Task<IActionResult> AllClassesOfBouquet(Guid BouquetId)
        {
            var result = await _classService.GetAllClassesOfBouquet(BouquetId);
            return Ok(result);
        }
        [HttpGet("GetAllClassesEmptyFromTeacher")]
        public async Task<IActionResult> GetAllClassesEmptyFromTeacher()
        {
            var result = await _classService.GetAllClassesEmptyFromTeacher();
            return Ok(result);
        }
        [HttpGet("GetAllClasses")]
        public async Task<IActionResult> GetAllClasses()
        {
            var result = await _classService.GetAllClasses();
            return Ok(result);
        }
        [HttpGet("GetClassById/{Id}")]
        public async Task<IActionResult> GetClassById(Guid Id)
        {
            var result = await _classService.GetClassByIdAsync(Id);


            return Ok(result);
        }


        // ✅ 2. إضافة  للحصة
        [HttpPost("AddClass")]
        public async Task<IActionResult> AddClass(AddClassDTO dTO)
        {
            var result = await _classService.AddClassAsync(dTO);
            return Ok(result);
        }
        [HttpPut("UpdateClass/{Id}")]
        public async Task<IActionResult> UpdateClass(Guid Id, UpdateClassDTO dTO)
        {
            var result = await _classService.UpdateClassAsync(Id, dTO);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteClass(Guid Id)
        {
            var result = await _classService.DeleteClass(Id);
            return Ok(result);
        }

        //// ✅ 3. حذف طالب من الحصة
        //[HttpDelete("{classId}/students/{studentId}")]
        //public async Task<IActionResult> RemoveStudentFromClass(Guid classId, Guid studentId)
        //{
        //    var result = await _classService.RemoveStudentFromClassAsync(classId, studentId);
        //    return Ok(result);
        //  }

        //// ✅ 4. ترقية طالب من قائمة الانتظار
        //[HttpPost("{classId}/promote")]
        //public async Task<IActionResult> PromoteStudentFromWaitingList(Guid classId)
        //{
        //    var result = await _classService.PromoteStudentFromWaitingListAsync(classId);
        //    return Ok(result);
        //}

        //// ✅ 5. تحديث بيانات الطالب في الحصة (الدفع مثلاً)
        //[HttpPut("{classId}/students/{studentId}")]
        //public async Task<IActionResult> UpdateStudentInClass(Guid classId, Guid studentId, [FromQuery] bool isPaid)
        //{
        //    var result = await _classService.UpdateStudentInClassAsync(classId, studentId, isPaid);
        //    return Ok(result);
        //}
        //[HttpGet("GetAllClassesByTeacherId")]
        //public async Task<IActionResult> GetAllClassesByTeacherId(Guid teacherId)
        //{

        //    var result = await _classService.GetAllClassesByTeacherId(teacherId);
        //    return Ok(result);


        //}

    }
}
