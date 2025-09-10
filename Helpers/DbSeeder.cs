using Microsoft.AspNetCore.Identity;

public static class IdentitySeeder
{
    public static async Task DbSeeder(RoleManager<IdentityRole<Guid>> roleManager)
    {
        var roles = new[] { "Admin", "Teacher", "Student" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                if (result.Succeeded)
                    Console.WriteLine($"Role '{role}' created successfully.");
                else
                    Console.WriteLine($"Failed to create role '{role}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            else
            {
                Console.WriteLine($"Role '{role}' already exists.");
            }
        }
    }

}

