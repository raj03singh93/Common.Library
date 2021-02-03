using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Library.Extensions
{
    public static class ListExtension
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data, string valueTypeDefaultColumn = "Value")
        {
            return new DataTable();
        }
    }
}
