using System;
using System.Runtime.Serialization;

namespace PostNLApi.Models.Response
{
    [DataContract]
    public class LabelResponse
    {
        /// <summary>
        /// Merged labels
        /// </summary>
        [DataMember(Name = "Barcode", IsRequired = false, EmitDefaultValue = false)]
        public MergedLabel[] MergedLabels { get; set; } = Array.Empty<MergedLabel>();
    
        /// <summary>
        /// Response shipments
        /// </summary>
        [DataMember(Name = "ResponseShipments", IsRequired = false, EmitDefaultValue = false)]
        public ResponseShipment[] ResponseShipments { get; set; } = Array.Empty<ResponseShipment>();
    }
}