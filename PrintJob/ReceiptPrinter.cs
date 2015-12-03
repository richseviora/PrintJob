using System;
using System.Globalization;
using System.IO;

namespace PrintJob
{
    public interface IReceiptGenerator
    {
        void OutputReceipt(Job job, string fileName);
    }

    public class ReceiptGenerator : IReceiptGenerator
    {
        public string OutputName { get; set; }

        public ReceiptGenerator(string outputName)
        {
            OutputName = outputName;

        }


        #region Implementation of IReceiptGenerator

        public void OutputReceipt(Job job, string fileName)
        {
            using (FileStream sb = new FileStream(fileName, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(sb))
                {
                    sw.AutoFlush = true;
                    OutputLine("Job Receipt", sw);
                    foreach (var jobItem in job.Items)
                    {
                        var priceOutput = RenderValue(jobItem.TotalPrice);
                        OutputLine($"{jobItem.Name}:  ${priceOutput} ", sw);
                    }
                    OutputLine($"Total: ${RenderValue(job.TotalPrice)}", sw);
                }
            }
        }

        #endregion

        /// <summary>
        /// Arguably not necessary but makes it quicker to swtich between WriteLine and hypothetical other option.
        /// </summary>
        /// <param name="line">Line to write.</param>
        /// <param name="writer">Writer to write to.</param>
        private void OutputLine(string line, StreamWriter writer)
        {
            writer.WriteLine(line);
        }

        private string RenderValue(decimal value)
        {
            return Math.Round(value, 2).ToString(CultureInfo.CurrentCulture);
        }
    }

}