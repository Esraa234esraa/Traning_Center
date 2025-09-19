using System.Linq.Expressions;
using TrainingCenterAPI.DTOs.Evaluation;
using TrainingCenterAPI.Helpers;
using TrainingCenterAPI.Models.evaluations;

namespace TrainingCenterAPI.Services.EvaluationsService
{
    public class EvaluationService : IEvaluationService
    {

        private readonly ApplicationDbContext _context;
        public ResponseDTO _ResponseDTO;


        public EvaluationService(ApplicationDbContext context)
        {
            _context = context;
            _ResponseDTO = new ResponseDTO();
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

        public async Task<ResponseModel<ResponseDTO>> GetAllEvaluation(GetAllEvaluationQuery request)
        {
            //filter


            var filterConditions = new Dictionary<Expression<Func<Evaluation, bool>>, bool>();

            if (!string.IsNullOrEmpty(request.SearchWord))
            {
                filterConditions.Add(x => x.evaluationOwner == request.SearchWord ||
                                          x.evaluationOwner.Contains(request.SearchWord)
                                          , true);
            }



            var evaluations = await _context.evaluations.Where(x => x.IsDeleted == false)

              .AsNoTracking().OrderByDescending(x => x.CreatedAt)
            .ApplyFilters(filterConditions, request.PageNumber, request.PageSize, ref _ResponseDTO).
              Select(item => new GetAllEvaluationDTO
              {

                  Id = item.Id,
                  evaluationOwner = item.evaluationOwner,
                  Rating = item.Rating,
                  Opnion = item.Opnion,
                  evaluationOwnerType = item.evaluationOwnerType,
                  CreatedAt = item.CreatedAt,
                  UpdatedAt = item.UpdatedAt,

              }).ToListAsync();
            if (evaluations == null || evaluations.Count() <= 0)
            {

                return ResponseModel<ResponseDTO>.FailResponse(" لاتوجد تقيمات");
            }

            _ResponseDTO.Result = evaluations;

            return ResponseModel<ResponseDTO>.SuccessResponse(_ResponseDTO, "تم رجوع التقيمات بنجاح");

        }
        public async Task<ResponseModel<List<GetAllEvaluationDTO>>> GetOnlyVisibleEvaluationsAsync()
        {
            var evaluations = await _context.evaluations.Where(x => x.IsDeleted == false && x.IsVisible == true)
                .OrderByDescending(x => x.CreatedAt)
                .AsNoTracking()

                .Select(c => new GetAllEvaluationDTO
                {
                    Id = c.Id,
                    evaluationOwner = c.evaluationOwner,
                    evaluationOwnerType = c.evaluationOwnerType,
                    Rating = c.Rating,
                    Opnion = c.Opnion,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,


                })
                .ToListAsync();
            if (evaluations.Count() <= 0)
                return ResponseModel<List<GetAllEvaluationDTO>>.FailResponse("لا توجد تقييمات ظاهرة");

            return ResponseModel<List<GetAllEvaluationDTO>>.SuccessResponse(evaluations, "Courses retrieved successfully");
        }

        public async Task<ResponseModel<bool>> HideEvaluationAsync(Guid id)
        {
            try
            {
                var evaluation = await _context.evaluations.FirstOrDefaultAsync(x => x.Id == id && x.IsVisible == true);
                if (evaluation == null)
                    return ResponseModel<bool>.FailResponse("التقييم ليس موجود او ليست ظاهره ");


                evaluation.IsVisible = false;
                _context.evaluations.Update(evaluation);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل التقييم الى  المخفيات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الخفي ");

            }


        }
        public async Task<ResponseModel<bool>> VisibleEvaluationAsync(Guid id)
        {
            try
            {
                var evaluation = await _context.evaluations.FirstOrDefaultAsync(x => x.Id == id && x.IsVisible == false);
                if (evaluation == null)
                    return ResponseModel<bool>.FailResponse("التقييم ليست موجود او ليس مخفي");

                // 👇 Soft delete

                evaluation.IsVisible = true;
                _context.evaluations.Update(evaluation);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل التقييم الى  الظهور");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الظهور ");

            }


        }
    }
}
