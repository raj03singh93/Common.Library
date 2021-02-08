using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Library.Extensions;

namespace Common.Library.Utility
{
    public static class ExcelOperation
    {
        public static DataTable ReadExcel(string path)
        {
            return new DataTable();
        }
        public static void ToExcel(this DataTable dt, string targetName)
        {

        }
        public static void ToExcel<T>(this IEnumerable<T> data, string targetName)
        {
            DataTable result = data.ToDataTable();

        }
    }
}
