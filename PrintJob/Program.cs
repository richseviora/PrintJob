using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace PrintJob
{
    class Program
    {

        const decimal TaxRate = 0.07m;
        const decimal MarginRate = 0.11m;
        const decimal ExtraMarginRate = 0.16m;

        static void Main(string[] args)
        {
            var fileName = string.Empty;
            while (fileName == string.Empty)
            {
                Console.WriteLine("Please type data file name");
                fileName = Console.ReadLine();
                if (!File.Exists(fileName))
                {
                    fileName = string.Empty;
                }
            }
            IFileLoader fileLoader = new FileLoader(fileName);
            fileLoader.Load();

            IJobLoader jobLoader = new JobLoader(marginRate: MarginRate, extraMarginRate: ExtraMarginRate, taxRate: TaxRate);
            Job newJob = jobLoader.Parse(fileLoader.Lines);
            
            IReceiptGenerator receiptGenerator = new ReceiptGenerator("receipt.txt");
            receiptGenerator.OutputReceipt(newJob);
        }
    }
}
