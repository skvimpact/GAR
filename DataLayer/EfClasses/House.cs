using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[GarItem("HOUSE")]
	[Table("HOUSES")]
	public class House
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ID")]
		public long Id { get; set; }
		[Column("OBJECTID")]
		public long ObjectId { get; set; }
		[Column("OBJECTGUID")]
		public string ObjectguId { get; set; } = string.Empty;
		[Column("CHANGEID")]
		public long ChangeId { get; set; }
		[Column("HOUSENUM")]
		public string? Housenum { get; set; }
		[Column("ADDNUM1")]
		public string? Addnum1 { get; set; }
		[Column("ADDNUM2")]
		public string? Addnum2 { get; set; }
		[Column("HOUSETYPE")]
		public int? Housetype { get; set; }
		[Column("ADDTYPE1")]
		public int? Addtype1 { get; set; }
		[Column("ADDTYPE2")]
		public int? Addtype2 { get; set; }
		[Column("OPERTYPEID")]
		public int OpertypeId { get; set; }
		[Column("PREVID")]
		public long? PrevId { get; set; }
		[Column("NEXTID")]
		public long? NextId { get; set; }
		[Column("UPDATEDATE")]
		public DateTime UpDateDate { get; set; }
		[Column("STARTDATE")]
		public DateTime StartDate { get; set; }
		[Column("ENDDATE")]
		public DateTime EndDate { get; set; }
		[Column("ISACTUAL")]
		public int Isactual { get; set; }
		[Column("ISACTIVE")]
		public int IsActive { get; set; }
	}
}

