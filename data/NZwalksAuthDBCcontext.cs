using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZwalks.API.data
{
    public class NZwalksAuthDBCcontext : IdentityDbContext
    {
        public NZwalksAuthDBCcontext(DbContextOptions<NZwalksAuthDBCcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "f301eab5-6fa2-4ef2-b5ed-113ae5b54eb0";
            var writerroleId = "89304bfe-01e0-4bb6-a7df-fd8942278e3d";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                },
                 new IdentityRole
                {
                    Id = writerroleId,
                    ConcurrencyStamp = writerroleId,
                    Name = "Writer",
                    NormalizedName = "writer".ToUpper(),
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
