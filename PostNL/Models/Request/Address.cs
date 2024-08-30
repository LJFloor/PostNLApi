using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PostNLApi.Models.Request
{
    /// <summary>
    /// Address for PostNL
    /// </summary>
    public class Address
    {
        /// <summary>
        /// The type of address
        /// </summary>
        /// <remarks>Please refer to <see cref="Request.AddressType"/> for the possible values.</remarks>
        [RegularExpression("^01|02|03|04|07|08|09$", ErrorMessage = "Invalid address type")]
        [Required]
        public string AddressType { get; set; }

        /// <summary>
        /// Area of the address
        /// </summary>
        [MaxLength(35)]
        public string Area { get; set; }

        /// <summary>
        /// Building name of the address
        /// </summary>
        [MaxLength(35)]
        public string BuildingName { get; set; }

        /// <summary>
        /// City of the address
        /// </summary>
        /// <example>Den Haag</example>
        [MaxLength(35)]
        public string City { get; set; }

        /// <summary>
        /// This field has a dependency with the field Name. One of both fields must be filled mandatory;
        /// using both fields is also allowed. Mandatory when AddressType is 09.
        /// </summary>
        /// <example>PostNL</example>
        [MaxLength(35)]
        public string CompanyName { get; set; }

        /// <summary>
        /// The ISO2 country code
        /// </summary>
        /// <example>NL</example>
        /// <remarks>https://www.iso.org/home.html</remarks>
        [RegularExpression("^[A-Za-z]{2}$", ErrorMessage = "Country code must be 2 letters")]
        [Required]
        public string CountryCode { get; set; }

        /// <summary>
        /// Send to specific department of a company
        /// </summary>
        [MaxLength(35)]
        public string Department { get; set; }

        /// <summary>
        /// Door code of address. Mandatory for some international shipments.
        /// </summary>
        [MaxLength(35)]
        public string DoorCode { get; set; }

        /// <summary>
        /// The first name for the address
        /// </summary>
        /// <remarks>Please add FirstName and Name (lastname) of the receiver to improve the parcel tracking experience of your customer.</remarks>
        [MaxLength(35)]
        public string FirstName { get; set; }

        /// <summary>
        /// Send to specific floor of a company
        /// </summary>
        [MaxLength(35)]
        public string Floor { get; set; }

        /// <summary>
        /// Mandatory for shipments to Benelux. Max. length is 5 characters (only for Benelux addresses).
        /// For Benelux addresses,this field should always be numeric.
        /// </summary>
        /// <example>3</example>
        [MaxLength(35)]
        [Required]
        public string HouseNr { get; set; }

        /// <summary>
        /// House number extension
        /// </summary>
        /// <example>B</example>
        [MaxLength(35)]
        public string HouseNrExt { get; set; }

        /// <summary>
        /// Last name of person. This field has a dependency with the field CompanyName. One of both fields must be filled mandatory;
        /// using both fields is also allowed.
        /// </summary>
        /// <remarks>Please add FirstName and Name (lastname) of the receiver to improve the parcel tracking experience of your customer.</remarks>
        [MaxLength(35)]
        public string Name { get; set; }

        /// <summary>
        /// Region of the address
        /// </summary>
        [MaxLength(35)]
        public string Region { get; set; }

        /// <summary>
        /// This field has a dependency with the field StreetHouseNrExt. One of both fields must be filled mandatory;
        /// using both fields simultaneously is discouraged.
        /// </summary>
        /// <example>Waldorpstraat</example>
        [MaxLength(35)]
        [Required]
        public string Street { get; set; }

        /// <summary>
        /// Zipcode of the address. Mandatory for shipments to Benelux. Max length (NL) 6 characters,(BE;LU) 4 numeric characters
        /// </summary>
        /// <example>2521CA</example>
        [MaxLength(17)]
        [Required]
        public string Zipcode { get; set; }
    }

    /// <summary>
    /// Address types for PostNL
    /// </summary>
    public abstract class AddressType
    {
        /// <summary>
        /// Receiver
        /// </summary>
        public const string Receiver = "01";

        /// <summary>
        /// Sender
        /// </summary>
        public const string Sender = "02";

        /// <summary>
        /// Alternative sender address
        /// </summary>
        public const string AlternativeSender = "03";

        /// <summary>
        /// Collection address
        /// </summary>
        public const string Collection = "04";

        /// <summary>
        /// Rerouted delivery address
        /// </summary>
        public const string ReroutedDelivery = "07";

        /// <summary>
        /// Return address
        /// </summary>
        public const string Return = "08";

        /// <summary>
        /// Delivery address (for use with Pick up at PostNL location)
        /// </summary>
        public const string PickupLocation = "09";
    }
}