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