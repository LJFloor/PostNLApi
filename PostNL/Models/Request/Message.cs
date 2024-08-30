using System;
using System.ComponentModel.DataAnnotations;

namespace PostNLApi.Models.Request
{
    public class Message
    {
        [Required]
        public string MessageID { get; set; } = "1";

        /// <summary>
        /// Date/time of sending the message.
        /// <remarks>Format: dd-mm-yyyy hh:mm:ss</remarks>
        /// </summary>
        [Required]
        public string MessageTimeStamp => DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");

        /// <summary>
        /// Printer type that will be used to process the label, e.g. Zebra printer or PDF
        /// </summary>
        /// <remarks>See Printer types for the available printer types.</remarks>
        [Required]
        public string PrinterType { get; set; } = "GraphicFile|PDF";
    }
}