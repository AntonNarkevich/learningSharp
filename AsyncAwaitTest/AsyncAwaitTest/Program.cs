using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<string> task = ReadTextAsync("2.txt");

            Console.WriteLine("Async method has been called. The flow has been returned to Main.");

            String st = task.Result;

            Console.WriteLine("Task has been finished.");

            Console.WriteLine(st);

            Console.Read();
        }

        static async Task<string> ReadTextAsync(string filePath)
        {
            Console.WriteLine("Async method has been started.");

            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true))
            {
                StringBuilder sb = new StringBuilder();

                byte[] buffer = new byte[0x1000];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                Console.WriteLine("Async method is going to be finished.");

                return sb.ToString();
            }
        }
    }
}
