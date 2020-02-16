using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BookSpider
{
    internal class Program
    {
       public static void Main(string[] args)
        {
            for (var i = 1; i <= 1000; i++)
                GetBookCode(i);
        }

        private static void GetBookCode(int bookNumber)
        {
            var urlBase = "http://www.gutenberg.org/files/" + bookNumber + "/";
            var filename = bookNumber + ".txt";
            var filename0 = bookNumber + "-0.txt";
            var filename5 = bookNumber + "-5.txt";
            var filename8 = bookNumber + "-8.txt";
            
            if(DownloadBook(urlBase, filename)) 
                Console.WriteLine(filename + " salvo com sucesso.");
            
            else if(DownloadBook(urlBase, filename0)) 
                Console.WriteLine(filename0 + " salvo com sucesso.");
            
            else if(DownloadBook(urlBase, filename5)) 
                Console.WriteLine(filename5 + " salvo com sucesso.");
            
            else if(DownloadBook(urlBase, filename8)) 
                Console.WriteLine(filename8 + " salvo com sucesso.");
            
            else 
                Console.WriteLine("Erro " + bookNumber);
        }
        
        private static bool DownloadBook(string urlBase, string filename)
        {
            var encoding = new UTF8Encoding();
            var requisicaoWeb = WebRequest.CreateHttp(urlBase + filename);         
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-GB;     rv:1.9.2.13) Gecko/20101203 Firefox/3.6.13 (.NET CLR 3.5.30729)";
            var docPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            var folder = @"\livros\";
            var fullPath = docPath + folder + filename;
            var line = "";
            List<string> lines = new List<string>();
            try
            {
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    var reader = new StreamReader(streamDados, encoding);
                    while ((line = reader.ReadLine()) != null)
                        lines.Add(line);
                    
                    File.WriteAllLines(fullPath, lines);
                    streamDados.Close();
                    resposta.Close();
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}