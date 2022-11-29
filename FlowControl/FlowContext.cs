//using System;
//using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace FlowControl
{
	public class FlowContext : DbContext
	{
        public DbSet<GarFile> GarFiles => Set<GarFile>();

        public FlowContext(DbContextOptions<FlowContext> options)
			: base((DbContextOptions<FlowContext>)options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("flow");
		}    
    }
}