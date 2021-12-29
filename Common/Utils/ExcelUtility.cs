using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Common.Utils
{
    /// <summary>
    /// Excel Utility
    /// </summary>
    public static class ExcelUtility
    {
        /// <summary>
        /// Reads excels.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Datatable of excel data.</returns>
        //public static DataTable ReadExcel(string filePath)
        //{
        //    OleDbConnection _conn = new OleDbConnection();
        //    DataSet Dset = new DataSet();
        //    try
        //    {
        //        string filename = Path.GetFileName(filePath);

        //        if ((filename.ToLower().EndsWith(".xls")))
        //            _conn = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source=" + filePath + "; Extended Properties=Excel 8.0;");
        //        else if ((filename.ToLower().EndsWith(".xlsx")))
        //            _conn = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + "; Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'");

        //        // -----------------------------------Select Data from Input Excel to dataset------------------------------------
        //        OleDbDataAdapter _cmd = new OleDbDataAdapter("Select * from [Sheet1$]", _conn);
        //        _conn.Open();
        //        _cmd.Fill(Dset);
        //        _conn.Close();
        //        return Dset.Tables[0];
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (_conn.State == ConnectionState.Open)
        //            _conn.Close();
        //    }
        //}

        /// <summary>
        /// Converts datatable to excel
        /// </summary>
        /// <param name="dt">datatable.</param>
        /// <param name="fileName">fileName.</param>
        //public static void ToExcel(this DataTable dt, string fileName)
        //{
        //    fileName = $"{fileName}.xlsx";
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        //Add DataTable in worksheet  
        //        wb.Worksheets.Add(dt, "Sheet1");
        //        wb.SaveAs(fileName);
        //    }
        //}
        /// <summary>
        /// Converts a list to excel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        //public static void ToExcel<T>(this IEnumerable<T> data, string fileName)
        //{
        //    DataTable dt = data.ToDataTable();
        //    fileName = $"{fileName}.xlsx";
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        //Add DataTable in worksheet  
        //        wb.Worksheets.Add(dt, "Sheet1");
        //        wb.SaveAs(fileName);
        //    }
        //}

        /// <summary>
        /// Converts datatable to CSV.
        /// </summary>
        /// <param name="dt">Datatable.</param>
        /// <param name="fileName">fileName.</param>
        public static void ToCSV(this DataTable dt, string fileName)
        {
            fileName = $"{fileName}.csv";
            var lines = new List<string>();

            string[] columnNames = dt.Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToArray();

            var header = string.Join(",", columnNames.Select(name => $"\"{name}\""));
            lines.Add(header);

            var valueLines = dt.AsEnumerable()
                .Select(row => string.Join(",", row.ItemArray.Select(val => $"\"{val}\"")));

            lines.AddRange(valueLines);

            File.WriteAllLines(fileName, lines);
        }
    }
}
