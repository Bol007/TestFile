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

        public async void Process()
        {
            var s = DateTime.Now;
            StepOne();
            var e = DateTime.Now;
            Console.WriteLine((e - s).TotalMinutes);
        }

        private async void StepOne()
        {
            var model = new Model1();
            var total = 100000000;
            var loopRec = 1000000;
            var nLoop = total / loopRec;

            for (int i = 1; i <= nLoop; i++)
            {
                var s = DateTime.Now;
                Console.WriteLine($"StepOne :  {i} ");
                // รอให้เสร็จ+
                await StepTwo(i);
                var e = DateTime.Now;
                Console.WriteLine("<<<<<<  "  +  (e - s).TotalMinutes);
            }

        }

        private async Task<bool> StepTwo(int a)
        {
            var model = new Model1();

            var total = 1000000;
            var loopRec = 100000;
            var nLoop = total / loopRec;
            var nFile = nLoop * a;
            var number = nLoop * (a - 1);

            for (int i = 1; i <= nLoop; i++)
            {
                int xx = number + i;
                Console.WriteLine($" Loop :  {xx} ");
                Task t = new Task(() => ReadAndWrite(xx));
                t.Start();
            }

            int fileCount = 0;
            do
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(FilePath);
                fileCount = dir.GetFiles().Length;
                Thread.Sleep(1000);
                Console.WriteLine($"file count {fileCount}");
            } while (fileCount < nFile);

            return true;
        }


        private async Task ReadAndWrite(int i = 0)
        {
            try
            {
                Console.WriteLine($"ReadAndWrite :  {i} ");
                var model = new Model1();
                var idata = model.CBS_LN_APP.AsNoTracking().OrderBy(q => q.CBS_APP_NO).Skip(0).Take(50000).ToList();

                var isAppend = !(i == 1);

                StringBuilder sb = new StringBuilder();
                sb.Append(ToCsv(idata, !isAppend));

                isAppend = false;
                WriteFile(FilePath + $"\\{FileName}{i}.txt", sb.ToString(), isAppend);

                Console.WriteLine($"Write success ({i})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Err ({i})  ({ex.Message})");
                throw;
           }

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

