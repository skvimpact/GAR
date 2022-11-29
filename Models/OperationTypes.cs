using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[Table("OPERATION_TYPES")]
	public class OperationTypes
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ID")]
		public int Id { get; set; }
		[Column("NAME")]
		public string Name { get; set; } = string.Empty;
		[Column("SHORTNAME")]
		public string? Shortname { get; set; }
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

