using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Razor9_identity.Models
{
    public class MyBlogContext :DbContext
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
			} 
    }
}