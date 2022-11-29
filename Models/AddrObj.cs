using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[Table("ADDR_OBJ")]
	public class AddrObj
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
		[Column("NAME")]
		public string Name { get; set; } = string.Empty;
		[Column("TYPENAME")]
		public string Typename { get; set; } = string.Empty;
		[Column("LEVEL")]
		public string Level { get; set; } = string.Empty;
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

