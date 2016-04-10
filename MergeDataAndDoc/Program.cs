//2016/03/12 Programer : 張弘瑜
//簡易版合併列印
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeDataAndDoc
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFileName = "defaultInput.txt";
            string outputFileName = "defaultOutput.txt";
            string templateFileName = "defaultTemplate.txt";
            if (args.Length == 6)//三個參數三個檔名
            {
                for (int i = 0; i < 6; i+=2)
                {
                   if(args[i]=="-i")
                       inputFileName = args[i + 1];
                  else if (args[i] == "-r")
                       outputFileName = args[i + 1];
                  else if (args[i] == "-t")
                       templateFileName = args[i + 1];
                }
            }
            
            using (StreamReader inputFile = new StreamReader(inputFileName, Encoding.Default))//讀input檔案
            using (StreamReader templateFile = new StreamReader(templateFileName, Encoding.Default))
            using (StreamWriter outputFile = new StreamWriter(outputFileName))
                MergeData(inputFile, templateFile, outputFile);
        }

        public static void MergeData(TextReader inputFile, TextReader templateFile, TextWriter outputFile)
        {
            string templateLine = templateFile.ReadToEnd();//read template file
            string tmp;//站存template 檔案
            string inputLine;
            string[] SplitLine; //存以[tab]作為分隔的字串
            SplitLine = inputFile.ReadLine().Split('\t');
            List<string> list = new List<string>(SplitLine);//第一次讀變數
            while ((inputLine = inputFile.ReadLine()) != null)
            {
                tmp = templateLine;
                SplitLine = inputLine.Split('\t');
                for (int i = 0; i < list.Count; i++)
                {
                    if (tmp.IndexOf(list[i]) > 0)//判斷有沒有這個字串
                        tmp = tmp.Replace(tmp.Substring(tmp.IndexOf("${" + list[i]), list[i].Length + 3)
                            , SplitLine[i]);
                }
                Console.WriteLine("Write line: " + tmp);
                outputFile.WriteLine(tmp);
            }
        }
    }
}
