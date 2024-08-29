using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PostNLApi.Models.Request
{
    public class Group
    {
        /// <summary>
        /// Group sort that determines the type of group that is indicated.
        /// </summary>
        /// <remarks>Possible values are 01 (Collection request), 03 (Multicollo) and 04 (Single parcel)</remarks>
        [RegularExpression("^01|03|04$", ErrorMessage = "Invalid group type. Possible values are 01 (Collection request), 03 (Multicollo) and 04 (Single parcel)")]
        [JsonRequired]
        public string GroupType { get; set; }

        /// <summary>
        /// Sequence number of the collo within the complete shipment (e.g. collo 2 of 4)
        /// </summary>
        /// <remarks>Mandatory for multicollo shipments</remarks>
        public int GroupSequence { get; set; }

        /// <summary>
        /// Total number of collo in the shipment (in case of multicollo shipments)
        /// </summary>
        /// <remarks>Mandatory for multicollo shipments</remarks>
        public int GroupCount { get; set; }

        /// <summary>
        /// Main barcode for the shipment (in case of multicollo shipments)
        /// </summary>
        /// <remarks>Mandatory for multicollo shipments</remarks>
        [MinLength(11)]
        [MaxLength(15)]
        [JsonRequired]
        public string MainBarcode { get; set; }
    }
}