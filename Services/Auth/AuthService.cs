namespace TrainingCenterAPI.Services.Auth
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly JwtService _jwtService;
        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            JwtService jwtService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;

        }

        public async Task<ResponseModel<RegisterResponseDto>> RegisterAsync(RegisterDto model)
        {
            Console.WriteLine("Step 1: Checking existing user...");
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return ResponseModel<RegisterResponseDto>.FailResponse("البريد الإلكتروني مسجل بالفعل.");

            Console.WriteLine("Step 2: Creating user object...");
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                Role = "Admin"
            };

            Console.WriteLine("Step 3: Creating user in DB...");
            var createResult = await _userManager.CreateAsync(user, model.Password);
            if (!createResult.Succeeded)
                return ResponseModel<RegisterResponseDto>.FailResponse(string.Join(", ", createResult.Errors.Select(e => e.Description)));

            Console.WriteLine("Step 4: Checking role exists...");
            var roleExists = await _roleManager.RoleExistsAsync(model.Role);
            if (!roleExists)
                return ResponseModel<RegisterResponseDto>.FailResponse("Role not found.");

            Console.WriteLine("Step 5: Assigning role...");
            await _userManager.AddToRoleAsync(user, model.Role);

            Console.WriteLine("Step 6: Preparing response...");
            var response = new RegisterResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = model.Role,
                CreatedAt = user.CreatedAt
            };

            Console.WriteLine("Step 7: Done.");

            // لو مش موجود يكمل التسجيل
            return ResponseModel<RegisterResponseDto>.SuccessResponse(response, "تم تسجيل المستخدم بنجاح.");
        }

        // Login
        public async Task<ResponseModel<LoginResponseDto>> LoginAsync(LoginDto model, HttpResponse httpResponse)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return ResponseModel<LoginResponseDto>.FailResponse("البريد الإلكتروني غير موجود");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                return ResponseModel<LoginResponseDto>.FailResponse("كلمة المرور غير صحيحة");
            }


            var roles = await _userManager.GetRolesAsync(user);

            //if (!roles.Contains("Admin"))
            //{
            //    return ResponseModel<LoginResponseDto>.FailResponse("Access denied: Only admins can login");
            //}

            var (token, expiresAt) = _jwtService.GenerateToken(user, roles);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.None, // علشان يشتغل في كل الحالات
                Expires = expiresAt
            };

            httpResponse.Cookies.Append("jwt", token, cookieOptions);

            var response = new LoginResponseDto
            {
                FullName = user.FullName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "User",
                ExpiresAt = expiresAt,
                Token = token,
            };

            return ResponseModel<LoginResponseDto>.SuccessResponse(response, "Login successful");
        }

        public async Task<ResponseModel<AdminDto>> MeAsync(ClaimsPrincipal userPrincipal)
        {
            var userId = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return ResponseModel<AdminDto>.FailResponse("Unauthorized");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ResponseModel<AdminDto>.FailResponse("User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin"))
            {
                return ResponseModel<AdminDto>.FailResponse("Access denied");
            }

            var adminDto = new AdminDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
            };

            return ResponseModel<AdminDto>.SuccessResponse(adminDto, "Admin profile retrieved successfully");
        }

        public ResponseModel<string> Logout(HttpResponse httpResponse)
        {
            httpResponse.Cookies.Delete("jwt", new CookieOptions
            {
                HttpOnly = true,
                Secure = false,              // نفس القيمة المستخدمة في الإنشاء
                SameSite = SameSiteMode.None, // نفس القيمة المستخدمة في الإنشاء
            });

            return ResponseModel<string>.SuccessResponse(null, "تم تسجيل الخروج بنجاح");
        }


        public async Task<ResponseModel<List<AdminDto>>> GetAllAdminsAsync()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");

            var result = admins
                .Where(a => a.IsActive)
                .Select(a => new AdminDto
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Email = a.Email,
                    PhoneNumber = a.PhoneNumber,
                    CreatedAt = a.CreatedAt
                }).ToList();
            if (result == null)
            {
                return ResponseModel<List<AdminDto>>.FailResponse(" admins not found");
            }


            return ResponseModel<List<AdminDto>>.SuccessResponse(result);
        }
        public async Task<ResponseModel<AdminDto>> GetAdminAsync(Guid Id)
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");

            var result = admins
                .Where(a => a.IsActive && a.Id == Id)
                .Select(a => new AdminDto
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Email = a.Email,
                    PhoneNumber = a.PhoneNumber,
                    CreatedAt = a.CreatedAt
                }).FirstOrDefault();
            if (result == null)
            {
                return ResponseModel<AdminDto>.FailResponse("this admin not found");
            }

            return ResponseModel<AdminDto>.SuccessResponse(result);
        }
        public async Task<ResponseModel<UpdateAdminDto>> UpdateAdminAsync(Guid id, UpdateAdminDto model)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return ResponseModel<UpdateAdminDto>.FailResponse("Admin not found.");

            // تحقق من البريد الإلكتروني الجديد (لو اختلف عن الحالي)
            if (!string.IsNullOrEmpty(model.Email) && model.Email != user.Email)
            {
                var emailExists = await _userManager.FindByEmailAsync(model.Email);
                if (emailExists != null)
                {
                    return ResponseModel<UpdateAdminDto>.FailResponse("Email is already taken.");
                }
                user.Email = model.Email;
                user.UserName = model.Email; // لا تنسي تحديث اسم المستخدم لو مرتبط بالايميل
            }

            user.FullName = model.FullName ?? user.FullName;
            user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return ResponseModel<UpdateAdminDto>.FailResponse("Failed to update admin.");

            var updated = new UpdateAdminDto
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return ResponseModel<UpdateAdminDto>.SuccessResponse(updated, "Admin updated successfully.");
        }

        public async Task<ResponseModel<string>> DeleteUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return ResponseModel<string>.FailResponse("User not found.");

            // تعديل حالة المستخدم بدلاً من الحذف
            user.IsActive = false;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return ResponseModel<string>.FailResponse("Failed to deactivate user.");

            return ResponseModel<string>.SuccessResponse("User deactivated successfully.");
        }

        public async Task<ResponseModel<string>> ChangePasswordAsync(Guid id, ChangePasswordDto model)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return ResponseModel<string>.FailResponse("User not found.");

            // تحقق من كلمة السر الحالية
            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (!passwordCheck)
                return ResponseModel<string>.FailResponse("Current password is incorrect.");

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
                return ResponseModel<string>.FailResponse(string.Join(", ", result.Errors.Select(e => e.Description)));

            return ResponseModel<string>.SuccessResponse(null, "Password changed successfully.");
        }


        public async Task<ResponseModel<AdminDto>> GetAdminByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return ResponseModel<AdminDto>.FailResponse("Admin not found.");

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin"))
                return ResponseModel<AdminDto>.FailResponse("Access denied: Not an admin.");

            var adminDto = new AdminDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt
            };

            return ResponseModel<AdminDto>.SuccessResponse(adminDto, "Admin fetched successfully.");
        }
    }

}