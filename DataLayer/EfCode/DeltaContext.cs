using System;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EfCode
{
	public class DeltaContext : DbContext
	{
		public DbSet<AddrObj>	AddrObjs	=> Set<AddrObj>();
		public DbSet<AdmHierarchy>	AdmHierarchy	=> Set<AdmHierarchy>();
		public DbSet<House>			Houses			=> Set<House>();
		public DbSet<HouseType>		HouseTypes		=> Set<HouseType>();
		public DbSet<MunHierarchy>	MunHierarchy	=> Set<MunHierarchy>();
		public DbSet<ObjectLevel>	ObjectLevels	=> Set<ObjectLevel>();
		public DbSet<Param>			Params			=> Set<Param>();
		public DbSet<ParamType>		ParamTypes		=> Set<ParamType>();

		public DeltaContext(DbContextOptions<DeltaContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("delta");
		}
	}
}

