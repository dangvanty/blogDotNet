using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Razor9_identity.Models
{
    public class MyBlogContext :IdentityDbContext<AppUser>
    {
			public MyBlogContext(DbContextOptions<MyBlogContext> options):base(options)
			{
					
			}
			public DbSet<Article> articles {set;get;}
			

			protected override void OnConfiguring(DbContextOptionsBuilder builder)
			{
					base.OnConfiguring(builder);

			}
			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
					base.OnModelCreating(modelBuilder);
					foreach (var entityType in modelBuilder.Model.GetEntityTypes())
					{
						var tableName = entityType.GetTableName();
						if(tableName.StartsWith("AspNet"))
						{
							entityType.SetTableName(tableName.Substring(6));
						}
					}
			} 
    }
}