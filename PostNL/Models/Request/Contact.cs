using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PostNLApi.Models.Request
{
    /// <summary>
    /// Contact for PostNL
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Type of the contact. This is a code.
        /// </summary>
        /// <remarks>Please refer to <see cref="Request.ContactType"/> for the possible values.</remarks>
        [RegularExpression("^01|02$", ErrorMessage = "Invalid contact type")]
        [Required]
        public string ContactType { get; set; }

        /// <summary>
        /// Email address of the contact.
        /// </summary>
        /// <remarks>Mandatory in some cases. Either the <see cref="Email"/> or <see cref="TelNr"/> needs to be filled in for Non EU destinations.</remarks>
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// Mobile phone number of the contact.
        /// </summary>
        [JsonProperty("SMSNr")]
        [MaxLength(17)]
        public string SmsNr { get; set; }

        /// <summary>
        /// Phone number of the contact.
        /// </summary>
        /// <remarks>
        /// Either the <see cref="Email"/> or <see cref="TelNr"/> needs to be filled in for Non EU destinations. Preferably prefixed with “+” and the
        /// international dialling code.
        /// </remarks>
        [MaxLength(17)]
        public string TelNr { get; set; }
    }

    /// <summary>
    /// Contact type for PostNL
    /// </summary>
    public abstract class ContactType
    {
        /// <summary>
        /// Receiver
        /// </summary>
        public const string Receiver = "01";

        /// <summary>
        /// Sender
        /// </summary>
        public const string Sender = "02";
    }
}