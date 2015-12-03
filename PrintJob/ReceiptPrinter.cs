using System;
using System.IO;

namespace PrintJob
{
    public interface IReceiptGenerator
    {
        void OutputReceipt(Job job);
    }

    public class ReceiptGenerator : IReceiptGenerator
    {
        FileStream Stream { get; set; }
        StreamWriter Writer { get; set; }

        public ReceiptGenerator(string outputName)
        {
            Stream = new FileStream(outputName, FileMode.Create);
            Writer = new StreamWriter(Stream);
            Writer.AutoFlush = true;
        }


        #region Implementation of IReceiptGenerator

        public void OutputReceipt(Job job)
        {
            // Renders Receipt
            OutputLine("Job Receipt");
            foreach (var jobItem in job.Items)
            {
                var priceOutput = RenderValue(jobItem.TotalPrice);
                OutputLine($"{jobItem.Name}:  ${priceOutput} ");
            }
            OutputLine($"Total: ${RenderValue(job.TotalPrice)}");
        }

        #endregion
        /// <summary>
        /// Generates a line in the stream writer.
        /// </summary>
        /// <param name="line">Line to add.</param>
        private void OutputLine(string line)
        {
            Console.WriteLine(line);
        }

        private string RenderValue(decimal value)
        {
            return Math.Round(value, 2).ToString();
        }
    }

}