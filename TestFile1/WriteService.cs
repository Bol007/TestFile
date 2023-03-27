using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestFile1
{
    public class WriteService
    {

        public string FilePath { get; set; }
        public string FileName { get; set; } = "x";

        public WriteService()
        {
            FilePath = Environment.CurrentDirectory + $"\\files\\{Guid.NewGuid()}";
            //FilePath = Environment.CurrentDirectory + $"\\files\\c993fd51-6bf9-4cc0-bd92-f304cdea6c81";
            System.IO.Directory.CreateDirectory(FilePath);
        }

        public void Process()
        {

            List<CBS_LN_APP> data = new List<CBS_LN_APP>();
            var model = new Model1();
            var loop = 100;
            var total = 50000;
            var nLoop = total / loop;
            var skip = 0;

            for (int i = 1; i <= 2000; i++)
            {
                Console.WriteLine($"Loop :  {i} ");
                var idata = model.CBS_LN_APP.AsNoTracking().OrderBy(q => q.CBS_APP_NO).Skip(0).Take(50000).ToList();
                skip = skip + loop;

                var isAppend = !(i == 1);

                write(idata, isAppend, i);

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

        private async Task write(IEnumerable<CBS_LN_APP> data, bool isAppend = false, int i = 0)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(ToCsv(data, !isAppend));

            isAppend = false;
            WriteFile(FilePath + $"\\{FileName}{i}.txt", sb.ToString(), isAppend);

            Console.WriteLine($"Write success ({i})");

        }

        private string ToCsv(IEnumerable<CBS_LN_APP> data, bool isHeader = true)
        {
            return Tranform.ToCsv<TestFile1.CBS_LN_APP>(",", data, isHeader);
        }

        private void WriteFile(string filePath, string allText, bool isAppend = false)
        {

            var isRetry = true;
            var iCount = 10;
            while (isRetry)
            {

                try
                {
                    using (StreamWriter sw = new StreamWriter(filePath, isAppend, Encoding.UTF8, 65536))
                    {
                        sw.WriteLine(allText);
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                    iCount = iCount - 1;
                    if (iCount == 0)
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

        public void MergeFile()
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(FilePath);
            int fileCount = dir.GetFiles().Length;
            var newFile = Environment.CurrentDirectory + "\\final.txt";
            //File.ReadAllLines(path);

            for (int i = 1; i <= fileCount; i++)
            {
                var fullpath = FilePath + $"\\{FileName}{i}.txt";
                Console.WriteLine($"Read ({fullpath})");

                var lines = File.ReadAllLines(fullpath); // ระวังใหญ่เกิน
                StringBuilder sb = new StringBuilder();
                foreach (var itemLine in lines)
                {
                    sb.Append(itemLine);
                }

                WriteFile(newFile, sb.ToString(), true);

            }
        }

    }

}

