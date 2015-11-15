using System;
using System.Collections.Generic;
using System.IO;

namespace PrintJob
{
    public class Job
    {

        public Job()
        {
            Items = new List<JobItem>();
        }

        public IList<JobItem> Items { get; set; }
        public bool ExtraMargin { get; set; }
        public decimal Margin { get; set; }
        public decimal TotalPrice { get; set; }

        public void AddItem(JobItem item)
        {
            this.Items.Add(item);
        }
       
        public void PrintReceipt()
        {

            FileStream filestream = new FileStream("Receipt.txt", FileMode.Create);
            var streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);

            Console.WriteLine("Job Receipt");
            foreach(var item in Items)
            {                   
                Console.WriteLine("{0}:  ${1} ", item.Name, Math.Round((item.Price + item.Tax),2));
            }
            Console.WriteLine("Total: ${0} ", Math.Round(TotalPrice,2));
        }
    }
}
