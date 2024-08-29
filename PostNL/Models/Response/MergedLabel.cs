using System.Runtime.Serialization;

namespace PostNLApi.Models.Response
{
    [DataContract]
    public class MergedLabel
    {
        /// <summary>
        /// List of barcodes that are merged
        /// </summary>
        [DataMember(Name = "Barcode", IsRequired = false, EmitDefaultValue = false)]
        public string[] Barcodes { get; set; }

        /// <summary>
        /// List of labels that are merged
        /// </summary>
        [DataMember(Name = "Customer", IsRequired = false, EmitDefaultValue = false)]
        public Label[] Labels { get; set; }
    }
}