using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[GarItem("ITEM")]
	[Table("ADM_HIERARCHY")]
	public class AdmHierarchy
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ID")]
		public long Id { get; set; }
		[Column("OBJECTID")]
		public long ObjectId { get; set; }
		[Column("PARENTOBJID")]
		public long? ParentobjId { get; set; }
		[Column("CHANGEID")]
		public long ChangeId { get; set; }
		[Column("REGIONCODE")]
		public string? Regioncode { get; set; }
		[Column("AREACODE")]
		public string? Areacode { get; set; }
		[Column("CITYCODE")]
		public string? Citycode { get; set; }
		[Column("PLACECODE")]
		public string? Placecode { get; set; }
		[Column("PLANCODE")]
		public string? Plancode { get; set; }
		[Column("STREETCODE")]
		public string? Streetcode { get; set; }
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
		[Column("ISACTIVE")]
		public int IsActive { get; set; }
		[Column("PATH")]
		public string Path { get; set; } = string.Empty;
	}
}

