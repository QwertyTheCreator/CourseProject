using Publications.Auth.Entities;

namespace Publications.Auth.Database.Seeds;

public class RoleSeeder(AppDbContext context) : ISeeder
{
    public void Seed()
    {
        if(context.Roles.Count() == 2)
        {
            return;
        }

        List<Role> roles =
        [
            new Role(){Id = Guid.Parse("56d82ace-40fc-4e1b-a307-938e8d673bde"), Title = "admin"},
            new Role(){Id = Guid.Parse("1797df4b-29fa-4c0a-87e7-2922f3ef3d62"), Title = "default"}
        ];

        context.Roles.AddRange(roles);
        context.SaveChanges();
    }
}
