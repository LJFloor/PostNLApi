using System.Collections.Generic;
using Newtonsoft.Json;

namespace PostNLApi.Models.Request
{
    /// <summary>
    /// Label request for PostNL
    /// </summary>
    public class LabelRequest
    {
        [JsonRequired]
        public Customer Customer { get; set; }

        [JsonRequired]
        public List<Shipment> Shipments { get; set; }

        [JsonRequired]
        public Message Message { get; set; } = new Message();

        /// <summary>
        /// Image of the label signature. This can be used to automatically sign the customs form.
        /// </summary>
        /// <remarks>Most file formats are supported, including JPG, PNG, BMP, WEBP, GIF</remarks>
        public byte[] LabelSignature { get; set; }

        /// <summary>
        /// If set to true, all shipments in the label request will be merged into a multi collo shipment. Else they will be treated as separate shipments.
        /// </summary>
        [JsonIgnore]
        public bool MultiCollo { get; internal set; } = false;

        /// <summary>
        /// If set to true, the shipment will be confirmed. Else it will be saved as a draft, and you have to confirm it manually.
        /// </summary>
        [JsonIgnore]
        public bool Confirm { get; internal set; } = true;
    }
}