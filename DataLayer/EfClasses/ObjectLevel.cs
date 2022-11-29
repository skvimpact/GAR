using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EfClasses
{
	[GarItem("OBJECTLEVEL")]
	[Table("OBJECT_LEVELS")]
	public class ObjectLevel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("LEVEL")]
		public int Level { get; set; }
		[Column("NAME")]
		public string Name { get; set; } = string.Empty;
		[Column("SHORTNAME")]
		public string? Shortname { get; set; }
		[Column("UPDATEDATE")]
		public DateTime UpDateDate { get; set; }
		[Column("STARTDATE")]
		public DateTime StartDate { get; set; }
		[Column("ENDDATE")]
		public DateTime EndDate { get; set; }
		[Column("ISACTIVE")]
		public bool IsActive { get; set; }
	}
}

