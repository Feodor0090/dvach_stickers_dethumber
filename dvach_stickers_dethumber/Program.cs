using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;

namespace dvach_stickers_dethumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Папка _files: ");
            Directory.SetCurrentDirectory(Console.ReadLine());
            Console.Write("URL стикерпака (директория с пнг): ");
            string baseUrl = Console.ReadLine();
            Console.WriteLine("Обработка...");
            var fs = new DirectoryInfo(Directory.GetCurrentDirectory()).EnumerateFiles("*_thumb.png");
            Directory.CreateDirectory("full");
            foreach (var f in fs)
            {
                string name = f.Name.Substring(0, f.Name.IndexOf("_thumb.png")) + ".png";
                HttpWebRequest httpRequest = WebRequest.CreateHttp(baseUrl + name);
                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream httpResponseStream = httpResponse.GetResponseStream();

                int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];
                int bytesRead = 0;

                FileStream fileStream = File.Create("full/" + name);
                while ((bytesRead = httpResponseStream.Read(buffer, 0, bufferSize)) != 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                }
            }
            Console.WriteLine("Готово.");
            Thread.Sleep(200);
        }
    }
}
