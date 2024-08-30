using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PostNLApi.Models.Request
{
    public class Amount
    {
        /// <summary>
        /// Amount type
        /// </summary>
        /// <remarks>Can be 01 (Cash on delivery) or 02 (insurance amount)</remarks>
        [RegularExpression("^01|02$")]
        [Required]
        public string AmountType { get; set; }

        /// <summary>
        /// Name of bank account owner
        /// </summary>
        [MaxLength(35)]
        public string AccountName { get; set; }

        /// <summary>
        /// BIC number
        /// </summary>
        /// <remarks>Optional for COD shipments, mandatory for bank account number other than originating in The Netherlands</remarks>
        [JsonProperty("BIC")]
        [MinLength(8)]
        [MaxLength(11)]
        public string Bic { get; set; }

        // TODO: capitalize in sanitized version
        /// <summary>
        /// Currency code
        /// </summary>
        /// <value>EUR, GBP, USD or CNY</value>
        [RegularExpression("^EUR|GBP|USD|CNY$")]
        public string Currency { get; set; } = "EUR";

        /// <summary>
        /// IBAN bank account number
        /// </summary>
        /// <remarks>Mandatory for COD shipments. Dutch IBAN numbers are 18 characters</remarks>
        // TODO: capitalize in sanitized version and remove spaces
        [RegularExpression(@"^[A-Z]{2}\d{2}[A-Z]{4}(\d|\s)*$", ErrorMessage = "Invalid IBAN")]
        [JsonProperty("IBAN")]
        public string Iban { get; set; }

        /// <summary>
        /// Personal payment reference
        /// </summary>
        [MaxLength(35)]
        public string Reference { get; set; }

        /// <summary>
        /// Transaction number
        /// </summary>
        [MaxLength(35)]
        public string TransactionNumber { get; set; }

        /// <summary>
        /// Money value in EUR (unless value <see cref="Currency"/> is specified for another currency)
        /// </summary>
        // TODO: math.Round to 2 decimals
        [Required]
        [Range(0, 999999.99)]
        public decimal Value { get; set; }
    }

    public abstract class AmountType
    {
        /// <summary>
        /// Cash on delivery (COD)
        /// </summary>
        public const string CashOnDelivery = "01";

        /// <summary>
        /// Insurance amount
        /// </summary>
        public const string InsuranceAmount = "02";
    }
}