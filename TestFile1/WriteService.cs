using System;
using System.Collections;
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

        public int recordCount { get; set; }
        public int remainCount { get; set; }
        public int loop { get; set; } = 1000000;
        public int nLoop { get; set; }
        public Queue<CBS_LN_APP> qItems { get; set; }


        public WriteService()
        {
            qItems = new Queue<CBS_LN_APP>();
        }

        public async void Process()
        {

            recordCount = await Init();
            remainCount = recordCount;
            Read();
            var result = await Write();

        }

        private async Task<int> Init()
        {
            var model = new Model1();
            var result = model.Database.SqlQuery<int>("select 600000000");
            return result.ToArray().First();
        }

        private async void Read()
        {
            await Task.Run(() =>
            {

                var model = new Model1();
                nLoop = recordCount / loop;
                var remain = recordCount % loop;
                if (remain > 0)
                {
                    nLoop++;
                }
                var skip = 0;

                for (int i = 1; i <= nLoop; i++)
                {
                    var data = model.CBS_LN_APP.AsNoTracking().OrderBy(q => q.CBS_APP_NO).Skip(0).Take(loop).ToList();
                    foreach (var idata in data)
                    {
                        qItems.Enqueue(idata);
                    }

                    skip = skip + loop;
                    Console.WriteLine($"Loop :  {i} , Data : {data.Count}");
                }

            });
        }

        private async Task<bool> Write()
        {
            var index = 1;
            var remain = recordCount % loop;
            while (remainCount > 0)
            {
                List<CBS_LN_APP> data = new List<CBS_LN_APP>();
                int numCheck = loop;
                if (index == nLoop)
                {
                    numCheck = remain;
                }

                if (qItems.Count >= numCheck)
                {

                    for (int i = 1; i <= numCheck; i++)
                    {
                        data.Add(qItems.Dequeue());
                    }

                    var isAppend = !(index == 1);

                    StringBuilder sb = new StringBuilder();
                    sb.Append(ToCsv(data, !isAppend));

                    string startupPath = Environment.CurrentDirectory;
                    WriteFile(Environment.CurrentDirectory + "\\x.txt", sb, isAppend);

                    Console.WriteLine($"Write success ({index}) ,remainCount ({remainCount})");
                    remainCount = remainCount - loop;
                    index = index + 1;
                    //Thread.Sleep(500);
                }
            }

            return true;
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

    }

}
