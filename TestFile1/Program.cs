using GSB.AlternativeScore.Utils.APIs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFile1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("input record count :");
            var input = Console.ReadLine();
            do
            {
                var s = DateTime.Now;
                ApiGateProxyBuilder apiBuilder = new ApiGateProxyBuilder("http://192.168.205.92/TestApi/api/");
                var api = apiBuilder.Build();

                var rec = int.Parse(input);
                var items = api.GetToTypeWithErr<List<CBS_LN_APP>>($"homes/{rec}");
                var e = DateTime.Now;

                Console.WriteLine($"============================");
                Console.WriteLine($"start : {s}");
                Console.WriteLine($"end : {e}");
                Console.WriteLine($"TotalSeconds  {(e - s).TotalSeconds}" );
                Console.WriteLine($"Record count : {items.Count}");
                Console.WriteLine($"============================");

                Console.Write("input record count :");
                 input = Console.ReadLine();

            } while (input != "e");

           

            Console.ReadLine();

        }

        //static void Main(string[] args)
        //{

        //    try
        //    {
        //        var s = DateTime.Now;
        //        var x = new WriteService();
        //        //x.Process();
        //        x.ReadFile();
        //        var e = DateTime.Now;
        //        Console.WriteLine((e - s).TotalMinutes);
        //    }
        //    catch (Exception   ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }

        //    Console.ReadLine();

        //}


        //        static void Main(string[] args)
        //    {
        //        var model = new Model1();
        //        List<CBS_LN_APP>data  = new List<CBS_LN_APP>() ;
        //        var s = DateTime.Now;
        //        //var data = model.Database.SqlQuery<CBS_LN_APP>("select top 100000 * from CBS_LN_APP").ToArray();
        //        //var x = model.CBS_LN_APP.AsNoTracking().OrderBy(q => q.CBS_APP_NO).Take(1000000).ToArray();
        //        //var x = DbContextExtensions.DataTable(model, "select top 1000000 * from CBS_LN_APP", null);

        //        var loop = 100000;
        //        var total = 1000000;
        //        var nLoop = total/ loop;
        //        var skip = 0;
        //        for (int i = 1; i <= nLoop; i++)
        //        {
        //             var idata = model.CBS_LN_APP.AsNoTracking().OrderBy(q => q.CBS_APP_NO).Skip(skip).Take(loop).ToList();
        //              skip=  skip+ loop;
        //              data.AddRange(idata);
        //        }

        //        var e = DateTime.Now;

        //        Console.WriteLine(data.Count);
        //        Console.WriteLine((e - s).TotalMinutes);

        //        s = DateTime.Now;
        //        StringBuilder sb = new StringBuilder();
        //        //foreach (var item in data)
        //        //{
        //        //   var j =   JsonConvert.SerializeObject(item);
        //        //    sb.Append(j.ToString());
        //        //}

        //        //List<CBS_LN_APP> items= new List<CBS_LN_APP>();
        //        sb.Append(ToCsv(data));
        //        //for (int i = 1; i <= 10; i++)
        //        //{
        //        //    items.AddRange(data);
        //        //    //sb.Append(ToCsv(data));
        //        //}

        //        e = DateTime.Now;
        //        Console.WriteLine((e - s).TotalMinutes);

        //        try
        //        {
        //            s = DateTime.Now;
        //            string startupPath = Environment.CurrentDirectory;

        //            WriteFile(Environment.CurrentDirectory + "\\x.txt", sb);
        //            e = DateTime.Now;
        //            Console.WriteLine((e - s).TotalMinutes);
        //        }
        //        catch (Exception ex ) 
        //        {
        //            Console.WriteLine(ex.Message);
        //        }


        //        Console.ReadLine();

        //    }

        //    static string  ToCsv(IEnumerable<CBS_LN_APP> data)
        //    {
        //        return Tranform.ToCsv<TestFile1.CBS_LN_APP>(",", data);
        //    }

        //    static void WriteFile(string filePath, StringBuilder sb)
        //    {
        //        using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8, 65536))
        //        {
        //            sw.WriteLine(sb.ToString());
        //        }

        //    }

        //}

    }
}
