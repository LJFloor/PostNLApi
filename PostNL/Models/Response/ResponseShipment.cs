using System;
using System.Runtime.Serialization;

namespace PostNLApi.Models.Response
{
    [DataContract]
    public class ResponseShipment
    {
        /// <summary>
        /// Barcode of the shipment
        /// </summary>
        [DataMember(Name = "Barcode", IsRequired = true, EmitDefaultValue = false)]
        public string Barcode { get; set; }

        /// <summary>
        /// List of labels of the shipment
        /// </summary>
        [DataMember(Name = "Labels", IsRequired = true, EmitDefaultValue = false)]
        public Label[] Labels { get; set; } = Array.Empty<Label>();

        /// <summary>
        /// The product code that is used for the delivery
        /// </summary>
        [DataMember(Name = "ProductCodeCollect", IsRequired = false, EmitDefaultValue = false)]
        public string ProductCodeDelivery { get; set; }
    }
}