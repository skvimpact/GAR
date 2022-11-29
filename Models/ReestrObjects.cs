using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[Table("REESTR_OBJECTS")]
	public class ReestrObjects
	{
		[Column("OBJECTID")]
		public long ObjectId { get; set; }
		[Column("CREATEDATE")]
		public DateTime CreateDate { get; set; }
		[Column("CHANGEID")]
		public long ChangeId { get; set; }
		[Column("LEVELID")]
		public int LevelId { get; set; }
		[Column("UPDATEDATE")]
		public DateTime UpDateDate { get; set; }
		[Column("OBJECTGUID")]
		public string ObjectguId { get; set; } = string.Empty;
		[Column("ISACTIVE")]
		public int IsActive { get; set; }
	}
}

