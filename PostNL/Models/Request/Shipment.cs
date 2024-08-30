﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using PostNLApi.Json;

namespace PostNLApi.Models.Request
{
    public class Shipment
    {
        /// <summary>
        /// List of 1 or more Address type elements
        /// </summary>
        /// <remarks>At least 1 address type is mandatory. See Address types for the available types.</remarks>
        [Required]
        public List<Address> Addresses { get; set; } = new List<Address>();

        /// <summary>
        /// List of amount types. An amount represents a value of the shipment. Amount type 01 mandatory for COD-shipments, Amount type 02 mandatory for domestic insured shipments.
        /// </summary>
        /// <remarks>https://developer.postnl.nl/docs/#/http/reference-data/reference-codes/amount-types</remarks>
        public List<Amount> Amounts { get; set; } = new List<Amount>();

        /// <summary>
        /// Barcode of the shipment. This is a unique value
        /// </summary>
        /// <remarks>If you leave this attribute out of your request a unique barcode will be generated automatically.</remarks>
        /// <example>3SDEVC748859096</example>
        [MinLength(11)]
        [MaxLength(16)]
        public string Barcode { get; set; }

        /// <summary>
        /// Code used for logistic purposes (usually generated by the service itself and returned in the response)
        /// </summary>
        /// <remarks>Please note that this must be provided when using the Confirm API to confirm shipments where coding texts are required (e.g. letterbox parcels).</remarks>
        [MaxLength(35)]
        public string CodingText { get; set; }

        /// <summary>
        /// Starting date/time of the collection of the shipment.
        /// </summary>
        [JsonConverter(typeof(PostNLDateTimeJsonConverter))]
        public DateTime? CollectionTimeStampStart { get; set; }

        /// <summary>
        /// Ending date/time of the collection of the shipment.
        /// </summary>
        [JsonConverter(typeof(PostNLDateTimeJsonConverter))]
        public DateTime? CollectionTimeStampEnd { get; set; }

        /// <summary>
        /// One or more ContactType elements belonging to a shipment.
        /// </summary>
        /// <remarks>Mandatory in some cases.</remarks>
        public IEnumerable<Contact> Contacts { get; set; } = new List<Contact>();

        /// <summary>
        /// Order number of the customer
        /// </summary>
        [MaxLength(35)]
        public string CustomerOrderNumber { get; set; }

        /// <summary>
        /// Dimension of the parcel
        /// </summary>
        /// <remarks>The maximum dimensions can be found in your PostNL contract.</remarks>
        [Required]
        public Dimension Dimension { get; set; }

        /// <summary>
        /// Product code of the shipment. See the https://developer.postnl.nl/docs/#/http/reference-data/product-codes for possible products.
        /// </summary>
        /// <example>3085</example>
        [RegularExpression(@"^\d{4,5}$", ErrorMessage = "Product code must be 4 or 5 digits")]
        [Required]
        public string ProductCodeDelivery { get; set; }

        /// <summary>
        /// List of 0 or more Group types with data, grouping multiple shipments together.
        /// </summary>
        /// <remarks>Mandatory for multicollo shipments</remarks>
        public Group[] Groups { get; set; }

        /// <summary>
        /// Customs information for non-EU shipments
        /// </summary>
        /// <remarks>The Customs type is mandatory for non-EU shipments and not allowed for any other shipment types.</remarks>
        public Customs Customs { get; set; }

        /// <summary>
        /// Product options for the shipment
        /// </summary>
        /// <remarks>mandatory for certain products</remarks>
        public List<ProductOption> ProductOptions { get; set; } = new List<ProductOption>();

        /// <summary>
        /// Date of birth.
        /// </summary>
        /// <remarks>Mandatory for Age check products</remarks>
        /// <example>08-08-2003</example>
        [RegularExpression(@"^([0-3]\d-[01]\d-[12]\d{3})$")]
        [JsonProperty("receiver_date_of_birth")]
        public string ReceiverDateOfBirth { get; set; }

        /// <summary>
        /// Your own reference of the shipment. This can be an invoice number or order number for example.
        /// </summary>
        /// <remarks>Mandatory for Extra@Home shipments; for E@H this is used to create your order number, so this should be unique for each request.</remarks>
        public string Reference { get; set; }

        /// <summary>
        /// Remark for the shipment
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Possibility to provide extra key-value pairs to the webservice.
        /// </summary>
        [Obsolete("Not used at the moment")]
        public List<KeyValuePair<string, string>> ExtraFields { get; set; } = new List<KeyValuePair<string, string>>();
    }
}