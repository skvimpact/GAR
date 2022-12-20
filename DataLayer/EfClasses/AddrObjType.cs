using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[GarItem("ADDRESSOBJECTTYPE")]
	[Table("ADDR_OBJ_TYPES")]
	public class AddrObjType
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ID")]
		public int Id { get; set; }
		[Column("LEVEL")]
		public int Level { get; set; }
		[Column("SHORTNAME")]
		public string Shortname { get; set; } = string.Empty;
		[Column("NAME")]
		public string Name { get; set; } = string.Empty;
		[Column("DESC")]
		public string? Desc { get; set; }
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

