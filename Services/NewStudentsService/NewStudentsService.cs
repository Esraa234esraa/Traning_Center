using TrainingCenterAPI.DTOs.NewStudents;
using TrainingCenterAPI.Models.Students;
using static TrainingCenterAPI.Enums.Enums;

namespace TrainingCenterAPI.Services.NewStudentsService
{
    public class NewStudentsService : INewStudentsService
    {
        private readonly ApplicationDbContext _context;


        public NewStudentsService(ApplicationDbContext context)
        {
            _context = context;


        }
        public async Task<ResponseModel<Guid>> AddNewStudent(PostNewStudentDTO DTO)
        {
            bool exists = await _context.newStudents.AnyAsync(s => s.Date == DTO.Date && s.Time == DTO.Time && s.IsDeleted == false);



            if (exists)
            {
                // already exists => reject
                return ResponseModel<Guid>.FailResponse("هذا الوقت محجوز");
            }
            if (DTO.Date.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday)
            {
                return ResponseModel<Guid>.FailResponse("لايمكن الحجز يوم الجمعة او السبت");

            }


            var newStudent = new NewStudent()
            {
                StudentName = DTO.StudentName,
                PhoneNumber = DTO.PhoneNumber,
                City = DTO.City,
                Date = DTO.Date,
                Time = DTO.Time,
                Gender = DTO.Gender,
                status = NewStudentStatus.New,


            };
            _context.newStudents.Add(newStudent);
            await _context.SaveChangesAsync();
            return ResponseModel<Guid>.SuccessResponse(newStudent.Id, "تمت الاضافة بنجاح");

        }

        public async Task<ResponseModel<string>> DeleteNewStudent(Guid Id)
        {
            var student = await _context.newStudents.FirstOrDefaultAsync(x => x.Id == Id && x.status == NewStudentStatus.New && x.IsDeleted == false);
            if (student == null)
            {

                return ResponseModel<string>.FailResponse("هذا الطالب غير موجود في الطلاب الجدد");
            }

            student.IsDeleted = true;
            _context.newStudents.Update(student);
            await _context.SaveChangesAsync();
            return ResponseModel<string>.SuccessResponse(" تم نقل الطالب الى سلة المهملات");
        }
        public async Task<ResponseModel<string>> DeleteWaitingStudent(Guid Id)
        {
            var student = await _context.newStudents.FirstOrDefaultAsync(x => x.Id == Id && x.status == NewStudentStatus.waiting && x.IsDeleted == false);
            if (student == null)
            {

                return ResponseModel<string>.FailResponse("هذا الطالب غير موجود في الطلاب المنتظرين");
            }

            student.IsDeleted = true;
            _context.newStudents.Update(student);
            await _context.SaveChangesAsync();
            return ResponseModel<string>.SuccessResponse(" تم نقل الطالب الى سلة المهملات");
        }

        public async Task<ResponseModel<List<GetAllNewStudentDTO>>> GetAllNewStudent()
        {
            var newStudents = await _context.newStudents.Where(x => x.status == NewStudentStatus.New && x.IsDeleted == false)
                .Select(item => new GetAllNewStudentDTO
                {

                    Id = item.Id,
                    StudentName = item.StudentName,
                    PhoneNumber = item.PhoneNumber,
                    Gender = item.Gender,
                    City = item.City,
                    Date = item.Date,
                    Time = item.Time,
                    status = item.status,
                    CreatedAt = item.CreatedAt

                }).ToListAsync();
            if (newStudents == null || newStudents.Count() <= 0)
            {

                return ResponseModel<List<GetAllNewStudentDTO>>.FailResponse(" لاتوجد طلاب جدد");
            }

            return ResponseModel<List<GetAllNewStudentDTO>>.SuccessResponse(newStudents, "تم رجوع الطلاب الجدد بنجاح");

        }
        public async Task<ResponseModel<NewStudent>> GetWaitingNewStudentById(Guid id)
        {
            var waitingStudent = await _context.newStudents.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false && x.status == NewStudentStatus.waiting);
            if (waitingStudent == null)
                return ResponseModel<NewStudent>.FailResponse(" ليست موجودة");


            return ResponseModel<NewStudent>.SuccessResponse(waitingStudent, "Courses retrieved successfully");
        }


        public async Task<ResponseModel<List<GetAllNewStudentDTO>>> GetAllWaitingNewStudent()
        {

            var newStudents = await _context.newStudents.Where(x => x.status == NewStudentStatus.waiting && x.IsDeleted == false)
                .Select(item => new GetAllNewStudentDTO
                {

                    Id = item.Id,
                    StudentName = item.StudentName,
                    PhoneNumber = item.PhoneNumber,
                    Gender = item.Gender,
                    City = item.City,
                    Date = item.Date,
                    Time = item.Time,
                    status = item.status,
                    CreatedAt = item.CreatedAt


                }).ToListAsync();
            if (newStudents == null || newStudents.Count() <= 0)
            {

                return ResponseModel<List<GetAllNewStudentDTO>>.FailResponse(" لاتوجد طلاب في الانتظار");
            }

            return ResponseModel<List<GetAllNewStudentDTO>>.SuccessResponse(newStudents, "تم رجوع الطلاب قائمة  الانتظار  بنجاح");
        }

        public async Task<ResponseModel<string>> MoveNewStudentToWaitingStudent(Guid Id)
        {
            var student = await _context.newStudents.FirstOrDefaultAsync(x => x.Id == Id && x.status == NewStudentStatus.New && x.IsDeleted == false);
            if (student == null)
            {

                return ResponseModel<string>.SuccessResponse("هذا الطالب غير موجود في الطلاب الجدد");
            }

            student.status = NewStudentStatus.waiting;
            _context.newStudents.Update(student);
            await _context.SaveChangesAsync();
            return ResponseModel<string>.SuccessResponse(" تم نقل الطالب الى  قائمة الانتظار");
        }

        public async Task<ResponseModel<Guid>> PutNewStudent(PutNewStudentDTO DTO, Guid Id)
        {

            var student = await _context.newStudents.FirstOrDefaultAsync(x => x.Id == Id && x.status == NewStudentStatus.New && x.IsDeleted == false);
            if (student == null)
            {

                return ResponseModel<Guid>.FailResponse("هذا الطالب غير موجود في الطلاب الجدد");
            }

            student.StudentName = DTO.StudentName;
            student.PhoneNumber = DTO.PhoneNumber;
            student.City = DTO.City;
            student.Date = DTO.Date;
            student.Time = DTO.Time;
            student.Gender = DTO.Gender;
            student.status = NewStudentStatus.New;
            _context.newStudents.Update(student);
            await _context.SaveChangesAsync();
            return ResponseModel<Guid>.SuccessResponse(student.Id, "تمت التعديل بنجاح ");
        }
        public async Task<ResponseModel<Guid>> PutWaitingStudent(PutNewStudentDTO DTO, Guid Id)
        {

            var student = await _context.newStudents.FirstOrDefaultAsync(x => x.Id == Id && x.status == NewStudentStatus.waiting && x.IsDeleted == false);
            if (student == null)
            {

                return ResponseModel<Guid>.FailResponse("هذا الطالب غير موجود في الطلاب المنتظرين");
            }

            student.StudentName = DTO.StudentName;
            student.PhoneNumber = DTO.PhoneNumber;
            student.City = DTO.City;
            student.Date = DTO.Date;
            student.Time = DTO.Time;
            student.Gender = DTO.Gender;
            student.status = NewStudentStatus.New;
            _context.newStudents.Update(student);
            await _context.SaveChangesAsync();
            return ResponseModel<Guid>.SuccessResponse(student.Id, "تمت التعديل بنجاح ");
        }
    }
}
