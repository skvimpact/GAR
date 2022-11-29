using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[GarItem("PARAMTYPE")]
	[Table("PARAM_TYPES")]
	public class ParamTypes
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ID")]
		public int Id { get; set; }
		[Column("NAME")]
		public string Name { get; set; } = string.Empty;
		[Column("CODE")]
		public string Code { get; set; } = string.Empty;
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

