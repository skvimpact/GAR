using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[Table("ROOMS")]
	public class Rooms
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
		[Column("NUMBER")]
		public string Number { get; set; } = string.Empty;
		[Column("ROOMTYPE")]
		public int Roomtype { get; set; }
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

