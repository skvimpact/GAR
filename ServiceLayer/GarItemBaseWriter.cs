using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public class GarItemBaseWriter
    {
        private readonly DeltaContext _deltaContext;
		private readonly GarContext _garContext;
		private SqlBulkCopy sqlBulkCopy;
		private readonly ScriptFactory scriptFactory;
		private readonly string _destinationTable;
		public GarItemBaseWriter(
            DeltaContext deltaContext,
			GarContext garContext,

			string destinationTable) {
            _deltaContext = deltaContext;
			_garContext = garContext;
			_destinationTable = destinationTable;
			sqlBulkCopy = new SqlBulkCopy(_deltaContext.Database.GetDbConnection().ConnectionString);
			sqlBulkCopy.DestinationTableName = $"[{_deltaContext.Model.GetDefaultSchema()}].[{destinationTable}]";

			_deltaContext.Model.GetEntityTypes()?.Where(et => et.GetTableName()  == destinationTable)?
                .SingleOrDefault()?
                    .GetProperties()
                    .Select(p => p.GetColumnBaseName())
                    .ToList()
				    .ForEach(f => sqlBulkCopy.ColumnMappings.Add(f, f));
			scriptFactory = new ScriptFactory(_deltaContext, _garContext);
		}
		public void Flush(DataTable dt)
        {
			TruncateTable(_destinationTable);
			DataTableReader dtr = new DataTableReader(dt);
		    sqlBulkCopy.WriteToServer(dtr);
            dt.Clear();
			MergeTable(_destinationTable);
		}

		public int TruncateTable(string tableName) =>
			_deltaContext.Database.ExecuteSqlRaw(scriptFactory.Truncate(tableName));

		public int MergeTable(string tableName)
		{
			_deltaContext.Database.SetCommandTimeout(300);
			return _deltaContext.Database.ExecuteSqlRaw(scriptFactory.Merge(tableName));
		}
	}
}