using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EfClasses
{
	[Table("ADDR_OBJ_DIVISION")]
	public class AddrObjDivision
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ID")]
		public long Id { get; set; }
		[Column("PARENTID")]
		public long ParentId { get; set; }
		[Column("CHILDID")]
		public long ChildId { get; set; }
		[Column("CHANGEID")]
		public long ChangeId { get; set; }
	}
}

