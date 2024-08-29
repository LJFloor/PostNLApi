using Newtonsoft.Json;

namespace PostNLApi.Models.Request
{
    /// <summary>
    /// Customer information for PostNL
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Code of delivery location at PostNL Pakketten
        /// </summary>
        /// <example>123456</example>
        public string CollectionLocation { get; set; }

        /// <summary>
        /// Name of customer contact person
        /// </summary>
        /// <example>Janssen</example>
        public string ContactPerson { get; set; }

        /// <summary>
        /// Customer code as known at PostNL Pakketten
        /// </summary>
        /// <example>DEVC</example>
        [JsonRequired]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Customer number
        /// </summary>
        /// <example>11223344</example>
        [JsonRequired]
        public string CustomerNumber { get; set; }

        /// <summary>
        /// E-mail address of the customer
        /// </summary>
        /// <example>email@company.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Customer name
        /// </summary>
        /// <example>Janssen</example>
        public string Name { get; set; }
    }
}