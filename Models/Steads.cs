using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[Table("STEADS")]
	public class Steads
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ID")]
		public int Id { get; set; }
		[Column("OBJECTID")]
		public int ObjectId { get; set; }
		[Column("OBJECTGUID")]
		public string ObjectguId { get; set; } = string.Empty;
		[Column("CHANGEID")]
		public int ChangeId { get; set; }
		[Column("NUMBER")]
		public string Number { get; set; } = string.Empty;
		[Column("OPERTYPEID")]
		public string OpertypeId { get; set; } = string.Empty;
		[Column("PREVID")]
		public int? PrevId { get; set; }
		[Column("NEXTID")]
		public int? NextId { get; set; }
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

