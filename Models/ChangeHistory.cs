using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[Table("CHANGE_HISTORY")]
	public class ChangeHistory
	{
		[Column("CHANGEID")]
		public long ChangeId { get; set; }
		[Column("OBJECTID")]
		public long ObjectId { get; set; }
		[Column("ADROBJECTID")]
		public string AdrobjectId { get; set; } = string.Empty;
		[Column("OPERTYPEID")]
		public int OpertypeId { get; set; }
		[Column("NDOCID")]
		public long? NdocId { get; set; }
		[Column("CHANGEDATE")]
		public DateTime ChangeDate { get; set; }
	}
}

