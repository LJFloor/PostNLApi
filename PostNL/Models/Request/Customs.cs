using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PostNLApi.Models.Request
{
    public class Customs
    {
        /// <summary>
        /// Certificate number. For specific items an export certificate is obliged, as proof that the item complies to the sanity regulations, more information.
        /// </summary>
        /// <remarks>
        /// Mandatory for Parcel shipments in the category type Commercial Goods, Commercial Sample and Returned Goods (Certificate,
        /// Invoice or License; at least 1 out of 3 must be selected)
        /// </remarks>
        [MaxLength(35)]
        public string CertificateNr { get; set; }

        public bool Certificate => !string.IsNullOrWhiteSpace(CertificateNr?.Trim());

        /// <summary>
        /// Fill in the invoice number of the shipment. For a faster customs clearing process apply the invoice on the outside of the shipment. 
        /// </summary>
        /// <remarks>
        /// Mandatory for Parcel shipments in the category type Commercial Goods, Commercial Sample and Returned Goods
        /// (Certificate, Invoice or License; at least 1 out of 3 must be selected).
        /// </remarks>
        [MaxLength(35)]
        public string LicenseNr { get; set; }

        public bool License => !string.IsNullOrWhiteSpace(LicenseNr?.Trim());

        /// <summary>
        /// Determines what to do when the shipment cannot be delivered the first time (if this is set to true,the shipment will be returned after the first failed attempt)
        /// </summary>
        public bool HandleAsNonDeliverable { get; set; }

        /// <summary>
        /// Currency code, only EUR and USS are allowed
        /// </summary>
        [RegularExpression("^EUR|USS$", ErrorMessage = "Currency must be EUR or USS")]
        public string Currency { get; set; }

        /// <summary>
        /// Type of shipment. Possible values: Gift,Documents,Commercial Goods,Commercial Sample,Returned Goods
        /// </summary>
        [RegularExpression("^Gift|Documents|Commercial Goods|Commercial Sample|Returned Goods$",
            ErrorMessage = "Invalid shipment type. Possible values: Gift,Documents,Commercial Goods,Commercial Sample,Returned Goods")]
        [Required]
        public string ShipmentType { get; set; }

        /// <summary>
        /// Trusted shipper ID
        /// </summary>
        /// <remarks>
        /// Use only when available. Depending on the destination with this ID the customs process can be faster. Only fill in this customs reference number if the sender
        /// is registered as Trusted Shipper in the country of destination.
        /// </remarks>
        [MaxLength(50)]
        public string TrustedShipperID { get; set; }

        /// <summary>
        /// Importer reference code.
        /// </summary>
        /// <remarks>
        /// Fill in a Tax Code or VAT number or Importer code. Depending on the destination with this reference the customs process can be faster.
        /// </remarks>
        [MaxLength(50)]
        public string ImporterReferenceCode { get; set; }

        /// <summary>
        /// The contents of the shipment.
        /// </summary>
        /// <remarks>The contents of the shipment. This section is mandatory (minimum once, maximum 5).</remarks>
        [Required]
        public Content[] Content { get; set; }
    }
}