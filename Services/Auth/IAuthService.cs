

namespace TrainingCenterAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<ResponseModel<RegisterResponseDto>> RegisterAsync(RegisterDto model);
        Task<ResponseModel<LoginResponseDto>> LoginAsync(LoginDto model, HttpResponse httpResponse);
        Task<ResponseModel<List<AdminDto>>> GetAllAdminsAsync(); // ← صح كده
        Task<ResponseModel<UpdateAdminDto>> UpdateAdminAsync(Guid id, UpdateAdminDto model);
        Task<ResponseModel<string>> DeleteUserAsync(Guid id); // هنحتاجها كمان
        Task<ResponseModel<string>> ChangePasswordAsync(Guid id, ChangePasswordDto model);
        Task<ResponseModel<AdminDto>> MeAsync(ClaimsPrincipal userPrincipal);
        ResponseModel<string> Logout(HttpResponse httpResponse);
        Task<ResponseModel<AdminDto>> GetAdminByIdAsync(Guid id);
    }


}

