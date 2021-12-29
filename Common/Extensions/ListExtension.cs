using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Common.Extensions
{
    /// <summary>
    /// List extentions.
    /// </summary>
    public static class ListExtension
    {

        /// <summary>
        /// Converts a generic list to datatable.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="data">data</param>
        /// <param name="valueTypeDefaultColumn">valueTypeDefaultColumn</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> data, string valueTypeDefaultColumn = "Value")
        {
            DataTable result = new DataTable();

            //handling value type and string
            if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {
                DataColumn dc = new DataColumn(valueTypeDefaultColumn);
                result.Columns.Add(dc);
                foreach (T item in data)
                {
                    DataRow row = result.NewRow();
                    row[0] = item;
                    result.Rows.Add(row);
                }
            }
            else
            {
                //Converting Complex Type to data table
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in properties)
                {
                    result.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                foreach (T item in data)
                {
                    DataRow row = result.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        try
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                        catch (Exception)
                        {

                            row[prop.Name] = DBNull.Value;
                        }
                    }
                    result.Rows.Add(row);
                }
            }
            return result;
        }
    }
}
