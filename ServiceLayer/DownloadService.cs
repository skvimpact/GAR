using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using DataLayer.EfClasses;
using DataLayer.EfCode;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using ServiceLayer.Infrastructure;

namespace ServiceLayer;
public class DownloadService
{
	private readonly ILogger<DownloadService> logger;
	private readonly DeltaContext deltaContext;
	private readonly GarContext garContext;
	private readonly ScriptFactory scriptFactory;
	private IDictionary<string, GarItemXmlReader> handlers = new Dictionary<string, GarItemXmlReader>();
	public DownloadService(ILogger<DownloadService> logger, DeltaContext deltaContext, GarContext garContext)
	{
		this.deltaContext = deltaContext;
		this.garContext = garContext;
		this.logger = logger;
		scriptFactory = new ScriptFactory(deltaContext, garContext);
		
		foreach (var entityType in deltaContext.Model.GetEntityTypes())
		{
			var table = entityType.GetTableName() ?? string.Empty;
			var fields = entityType.GetProperties().Select(p => p.GetColumnBaseName()).ToList();
			handlers.Add(table, new GarItemXmlReader(
				entityType.ClrType.Class<GarItemAttribute>()?.Name ?? string.Empty,
				entityType.GetProperties().Select(p => p.GetColumnBaseName()),
				new GarItemBaseWriter(deltaContext, garContext, table).Flush
				/*dt => {
					//TruncateTable(table);
					new GarItemBaseWriter(deltaContext, table).Write(dt);
					//MergeTable(table);
				}*/));
		}
	}
	public void HandleZipFile(string zipFileName)
	{
		logger.LogInformation($"Starting {zipFileName}...");
		using (ZipArchive zip = ZipFile.Open(zipFileName, ZipArchiveMode.Read))
		{
			var all = zip.Entries.Count();
			var count = 0;
    		foreach (ZipArchiveEntry entry in zip.Entries)
			{
				var tableName = GetTableName(entry.Name);
				count++;
        		if(handlers.ContainsKey(tableName))
				{
					
					logger.LogInformation($"{count}({all}) {entry.FullName}");
            		entry.ExtractToFile(entry.Name, true);
					HandleFile(entry.Name);
					File.Delete(entry.Name);
				}
			}
		}
		logger.LogInformation($"Done {zipFileName}.");
	}
	public void HandleFile(string fileName)
	{
		var table = GetTableName(fileName);
		//TruncateTable(table);
		handlers[table].Read(fileName);
		//MergeTable(table);
	}
	public int TruncateTable(string tableName) =>
		deltaContext.Database.ExecuteSqlRaw(scriptFactory.Truncate(tableName));
	public int MergeTable(string tableName) 
	{
		deltaContext.Database.SetCommandTimeout(300);
		return deltaContext.Database.ExecuteSqlRaw(scriptFactory.Merge(tableName));
	}
	public long Count(string tableName)
	{
		var param = new SqlParameter
		{
			ParameterName = "@count",
			SqlDbType = SqlDbType.BigInt,
			Direction = ParameterDirection.Output
		};

		deltaContext.Database.ExecuteSqlRaw(scriptFactory.Count(tableName), param);
		return (long)param.Value;
	}
    public string GetTableName(string file) =>        
        new Regex(@"^AS_[a-zA-Z_]+[^_\d{1,}]")
            .Match(Path.GetFileNameWithoutExtension(file))
            .Value
            .Replace("AS_", "");	
}
