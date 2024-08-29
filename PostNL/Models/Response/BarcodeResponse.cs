using System.Runtime.Serialization;

namespace PostNLApi.Models.Response
{
    [DataContract]
    public class BarcodeResponse
    {
        [DataMember(Name = "Barcode", IsRequired = true, EmitDefaultValue = false)]
        public string Barcode { get; set; }
    }
}