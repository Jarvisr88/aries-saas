namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.ClipboardSource.SpreadsheetML;
    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml.Linq;

    internal class XmlClipboardSource : IClipboardSource
    {
        public IClipboardData GetData()
        {
            string str;
            ClipboardData data = new ClipboardData();
            using (StreamReader reader = new StreamReader(Clipboard.GetData("XML Spreadsheet") as MemoryStream))
            {
                str = reader.ReadToEnd();
            }
            foreach (Worksheet worksheet in new Workbook(XDocument.Parse(str.Trim(new char[1]))).Worksheets.Values)
            {
                int num = 0;
                while (num < worksheet.Table.Rows.Count)
                {
                    object[] rowCells = new object[worksheet.Table.Rows[num].Cells.Count];
                    int index = 0;
                    while (true)
                    {
                        if (index >= worksheet.Table.Rows[num].Cells.Count)
                        {
                            data.AddRow(new ClipboardRow(rowCells));
                            num++;
                            break;
                        }
                        rowCells[index] = worksheet.Table[num][index].Value;
                        index++;
                    }
                }
            }
            return data;
        }
    }
}

