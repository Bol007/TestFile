using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestFile1
{
    public class WriteService
    {

        public void Process()
        {

            List<CBS_LN_APP> data = new List<CBS_LN_APP>();
            var model = new Model1();
            var loop = 100000;
            var total = 1000000;
            var nLoop = total / loop;
            var skip = 0;

            for (int i = 1; i <= 600; i++)
            {
                var idata = model.CBS_LN_APP.AsNoTracking().OrderBy(q => q.CBS_APP_NO).Skip(0).Take(1000000).ToList();
                skip = skip + loop;

                var isAppend = !(i == 1);
                write(idata, isAppend,i);

                Console.WriteLine($"Loop :  {i} , Data : {idata.Count}");
            }

            //for (int i = 1; i <= nLoop; i++)
            //{
            //    var idata = model.CBS_LN_APP.AsNoTracking().OrderBy(q => q.CBS_APP_NO).Skip(skip).Take(loop).ToList();
            //    skip = skip + loop;

            //    var isAppend = !(i == 1);
            //    write(idata, isAppend);

            //    Console.WriteLine($"Loop :  {i} , Data : {idata.Count}");
            //}

        }

        private async void write(IEnumerable<CBS_LN_APP> data, bool isAppend = false, int i=0)
        {

            await Task.Run(() =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ToCsv(data, !isAppend));

                string startupPath = Environment.CurrentDirectory;

                WriteFile(Environment.CurrentDirectory + "\\x.txt", sb, isAppend);

                Console.WriteLine($"Write success ({i})");
            });

        }

        private string ToCsv(IEnumerable<CBS_LN_APP> data, bool isHeader = true)
        {
            return Tranform.ToCsv<TestFile1.CBS_LN_APP>(",", data, isHeader);
        }

        private void WriteFile(string filePath, StringBuilder sb, bool isAppend = false)
        {

            var isRetry = true;
            var iCount = 10;
            while (isRetry)
            {

                try
                {
                    using (StreamWriter sw = new StreamWriter(filePath, isAppend, Encoding.UTF8, 65536))
                    {
                        sw.WriteLine(sb.ToString());
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                    iCount = iCount - 1;
                    if (iCount   ==  0)
                    {
                        isRetry = false;
                    }
                }
                finally
                {
                    isRetry = false;
                }

            }
        

        }

    }

}
