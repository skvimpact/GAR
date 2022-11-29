using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[GarItem("PARAM")]
	[Table("PARAM")]
	public class Param
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ID")]
		public long Id { get; set; }
		[Column("OBJECTID")]
		public long ObjectId { get; set; }
		[Column("CHANGEID")]
		public long? ChangeId { get; set; }
		[Column("CHANGEIDEND")]
		public long ChangeIdend { get; set; }
		[Column("TYPEID")]
		public int TypeId { get; set; }
		[Column("VALUE")]
		public string Value { get; set; } = string.Empty;
		[Column("UPDATEDATE")]
		public DateTime UpDateDate { get; set; }
		[Column("STARTDATE")]
		public DateTime StartDate { get; set; }
		[Column("ENDDATE")]
		public DateTime EndDate { get; set; }
	}
}

