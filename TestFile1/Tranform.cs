using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestFile1
{
    public static class Tranform
    {
        public static string ToCsv<T>(string separator, IEnumerable<T> objectlist,bool isHeader=true)
        {
            Type t = typeof(T);
            PropertyInfo[] p = t.GetProperties();

            // sort properties by name
            //Array.Sort(p,
            //        delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
            //        { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

            // write property names
            //foreach (PropertyInfo propertyInfo in p)
            //{
            //    System.Diagnostics.Debug.WriteLine(propertyInfo.Name);
            //}

            StringBuilder csvdata = new StringBuilder();

            if (isHeader)
            {
                string header = String.Join(separator, p.Select(f => f.Name).ToArray());
                csvdata.AppendLine(header);
            }

            var index = 1;
            var dataCount = objectlist.Count();
            foreach (var o in objectlist)
            {
                if (index != dataCount)
                {
                    csvdata.AppendLine(ToCsvFields(separator, p, o));
                }
                else
                {
                    csvdata.Append(ToCsvFields(separator, p, o));
                }
                index++;
            }

            return csvdata.ToString();
        }

        public static string ToCsvFields(string separator, PropertyInfo[] fields, object o)
        {
            StringBuilder linie = new StringBuilder();

            foreach (var f in fields)
            {
                if (linie.Length > 0)
                    linie.Append(separator);

                var x = f.GetValue(o);

                if (x != null)
                    linie.Append(x.ToString());
            }

            return linie.ToString();
        }

    }
}
