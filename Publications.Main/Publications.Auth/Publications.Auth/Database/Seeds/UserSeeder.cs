using Publications.Auth.Entities;
using Publications.Auth.Services;

namespace Publications.Auth.Database.Seeds;

public class UserSeeder(AppDbContext context) : ISeeder
{
    public void Seed()
    {
        if (context.Users.Any(x => x.Login == "admin"))
        {
            return;
        }

        var admin = new User()
        {
            Id = Guid.Parse("73ee08f3-7ade-4318-9ee7-c7a6157fbafd"),
            Login = "admin",
            PasswordHash = HashService.HashPassword("admin"),
            RoleId = Guid.Parse("56d82ace-40fc-4e1b-a307-938e8d673bde")
        };

        context.Users.Add(admin);
        context.SaveChanges();
    }
}
