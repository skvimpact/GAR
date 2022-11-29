using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ServiceLayer
{
    public class GarItemXmlReader
    {
        private string garItemName;
        private Action<DataTable> flushDataTable;
        private int bufferSize;
        public readonly DataTable DataTable = new DataTable();
        public GarItemXmlReader(
            string garItemName,
            IEnumerable<string> fields,
            Action<DataTable> flushDataTable,
            int bufferSize = 20000) {
            this.garItemName = garItemName;
            this.flushDataTable = flushDataTable;
            this.bufferSize = bufferSize;
			fields
				.ToList()
				.ForEach(f => DataTable.Columns.Add(f));            
        }

        public void Read(string fileName)
        {
            int count = 0;
	        using (XmlReader reader =
				XmlReader.Create(new FileStream(fileName, FileMode.Open),
					new XmlReaderSettings() { CloseInput = true })) {
				while (reader.Read())
				{
					if (reader.NodeType.Equals(XmlNodeType.Element))
					{
						if (reader.HasAttributes && reader.Name == garItemName)
						{
                            count++;
							DataRow dataRow = DataTable.NewRow();
							while (reader.MoveToNextAttribute())
							{
								dataRow[reader.Name] = reader.Value; 
							}
							DataTable.Rows.Add(dataRow);
							if (bufferSize != 0 && count % bufferSize == 0)
							{
								flushDataTable(DataTable);
							}
							reader.MoveToElement();
						}
					}
					if (reader.NodeType.Equals(XmlNodeType.EndElement))
					{
                        flushDataTable(DataTable);
					}
				}
			}            
        }
    }
}