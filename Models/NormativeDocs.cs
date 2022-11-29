using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[GarItem("NORMDOC")]
	[Table("NORMATIVE_DOCS")]
	public class NormativeDocs
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ID")]
		public long Id { get; set; }
		[Column("NAME")]
		public string Name { get; set; } = string.Empty;
		[Column("DATE")]
		public DateTime Date { get; set; }
		[Column("NUMBER")]
		public string Number { get; set; } = string.Empty;
		[Column("TYPE")]
		public int Type { get; set; }
		[Column("KIND")]
		public int Kind { get; set; }
		[Column("UPDATEDATE")]
		public DateTime UpDateDate { get; set; }
		[Column("ORGNAME")]
		public string? Orgname { get; set; }
		[Column("REGNUM")]
		public string? Regnum { get; set; }
		[Column("REGDATE")]
		public DateTime? RegDate { get; set; }
		[Column("ACCDATE")]
		public DateTime? AccDate { get; set; }
		[Column("COMMENT")]
		public string? Comment { get; set; }
	}
}

