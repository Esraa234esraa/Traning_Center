using TrainingCenterAPI.DTOs.Evaluation;
using TrainingCenterAPI.Models.evaluations;

namespace TrainingCenterAPI.Services.EvaluationsService
{
    public class EvaluationService : IEvaluationService
    {

        private readonly ApplicationDbContext _context;


        public EvaluationService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<ResponseModel<Guid>> AddEvaluation(PostEvaluationDTO DTO)
        {
            try
            {
                var ev = new Evaluation()
                {
                    evaluationOwner = DTO.evaluationOwner,
                    Opnion = DTO.Opnion,
                    Rating = DTO.Rating,
                    evaluationOwnerType = DTO.evaluationOwnerType,


                };
                _context.evaluations.Add(ev);
                await _context.SaveChangesAsync();
                return ResponseModel<Guid>.SuccessResponse(ev.Id, "تمت الاضافة بنجاح");
            }
            catch (Exception ex)
            {

                return ResponseModel<Guid>.FailResponse($" {ex.Message}فشل الاضافة ");
            }
        }

        public async Task<ResponseModel<string>> DeleteEvaluation(Guid Id)
        {
            var evaluation = await _context.evaluations.FirstOrDefaultAsync(x => x.Id == Id && x.IsDeleted == false);
            if (evaluation == null)
            {

                return ResponseModel<string>.FailResponse("هذا التقييم غير موجود   ");
            }

            evaluation.IsDeleted = true;
            _context.evaluations.Update(evaluation);
            await _context.SaveChangesAsync();
            return ResponseModel<string>.SuccessResponse(" تم نقل التقييم الى سلة المهملات");
        }

        public async Task<ResponseModel<List<GetAllEvaluationDTO>>> GetAllEvaluation()
        {
            var evaluations = await _context.evaluations.Where(x => x.IsDeleted == false)
              .AsNoTracking().Select(item => new GetAllEvaluationDTO
              {

                  Id = item.Id,
                  evaluationOwner = item.evaluationOwner,
                  Rating = item.Rating,
                  Opnion = item.Opnion,
                  evaluationOwnerType = item.evaluationOwnerType,
                  CreatedAt = item.CreatedAt

              }).ToListAsync();
            if (evaluations == null || evaluations.Count() <= 0)
            {

                return ResponseModel<List<GetAllEvaluationDTO>>.FailResponse(" لاتوجد تقيمات");
            }

            return ResponseModel<List<GetAllEvaluationDTO>>.SuccessResponse(evaluations, "تم رجوع التقيمات بنجاح");

        }
    }
}
