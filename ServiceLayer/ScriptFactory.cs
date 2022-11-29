using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public class ScriptFactory
    {
        private readonly DeltaContext deltaContext;
        private readonly GarContext garContext;
        public ScriptFactory(DeltaContext deltaContext, GarContext garContext)
        {
            this.deltaContext = deltaContext;
            this.garContext = garContext;
        }
        public string Count(string tableName)
        {
            return $"select @count = count(*) from [{deltaContext.Model.GetDefaultSchema()}].[{tableName}]";
        }
        public string Truncate(string tableName)
        {
            return $"truncate table [{deltaContext.Model.GetDefaultSchema()}].[{tableName}]";
        }                
        public string Merge(string tableName)
        {
            var schemaFrom = deltaContext.Model.GetDefaultSchema();
            var schemaTo = garContext.Model.GetDefaultSchema();
            var fields =  deltaContext.Model.GetEntityTypes()?.Where(et => et.GetTableName()  == tableName)?
                .SingleOrDefault()?
                    .GetProperties()
                    .Select(p => p.GetColumnBaseName())
                    .ToList();
            var keys =  deltaContext.Model.GetEntityTypes()?.Where(et => et.GetTableName()  == tableName)?
                .SingleOrDefault()?
                    .GetProperties()
                    .Where(p => p.IsPrimaryKey())
                    .Select(p => p.GetColumnBaseName())
                    .ToList();        
            var sb = new StringBuilder();
            //sb.Append($"if OBJECT_ID('[{schemaTo}].[{tableName}_Merge]', 'P') is not null\n");
            //sb.Append($"\tdrop procedure [{schemaTo}].[{tableName}_Merge]\n");
            //sb.Append("Go\n");
            //sb.Append("/* Tests\n");
            //sb.Append($"exec [{schemaTo}].[{tableName}_Merge]\n");
            //sb.Append("*/\n");
            //sb.Append($"create procedure [{schemaTo}].[{tableName}_Merge]\n");
            //sb.Append("as\n");
            //sb.Append("begin\n");
            //sb.Append("\tset nocount on;\n");
            sb.Append($"\tmerge into [{schemaTo}].[{tableName}] as Tgt\n");
            sb.Append($"\tusing [{schemaFrom}].[{tableName}] as Src on\n");
            sb.Append("\t(\n");
            sb.AppendJoin(" and\n", keys?.Select(x => $"\t\tTgt.[{x}] = Src.[{x}]") ?? new string[] {string.Empty});
            sb.Append("\n\t)\n");
            sb.Append("\twhen matched and\n");
            sb.Append("\t(\n");
            sb.AppendJoin(" or\n", fields?.Except(keys).Select(x => $"\t\tTgt.[{x}] <> Src.[{x}]") ?? new string[] {string.Empty});
            sb.Append("\n\t) then update\n");
            sb.Append("\tset\n");
            sb.AppendJoin(",\n", fields?.Except(keys).Select(x => $"\t\tTgt.[{x}] = Src.[{x}]") ?? new string[] {string.Empty});
            sb.Append("\n\twhen not matched then insert\n");
            sb.Append("\t(\n");
            sb.AppendJoin(",\n", fields?.Select(x => $"\t\t[{x}]") ?? new string[] {string.Empty});
            sb.Append("\n\t)\n");
            sb.Append("\tvalues\n");
            sb.Append("\t(\n");
            sb.AppendJoin(",\n", fields?.Select(x => $"\t\tSrc.[{x}]") ?? new string[] {string.Empty});
            sb.Append("\n\t);\n");
            //sb.Append("end\n");
            //sb.Append("Go\n");
            return sb.ToString();
        }
    }
}