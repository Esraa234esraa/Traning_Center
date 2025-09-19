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

        // ✅ 1. جلب حصة + طلابها
        [HttpGet("AllClassesOfBouquet")]
        public async Task<IActionResult> AllClassesOfBouquet(Guid BouquetId)
        {
            var result = await _classService.GetAllClassesOfBouquet(BouquetId);
            return Ok(result);
        }

        // ✅ 2. إضافة  للحصة
        [HttpPost("AddClass")]
        public async Task<IActionResult> AddClass(AddClassDTO dTO)
        {
            var result = await _classService.AddClassAsync(dTO);
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
