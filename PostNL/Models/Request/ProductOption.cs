using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PostNLApi.Models.Request
{
    public class ProductOption
    {
        /// <summary>
        /// The characteristic of the ProductOption.
        /// </summary>
        /// <example>118</example>
        [JsonProperty("characteristic")]
        [JsonRequired]
        [RegularExpression("^\\d{3}$")]
        public string Characteristic { get; set; }

        /// <summary>
        /// The product option code for this ProductOption.
        /// </summary>
        /// <example>006</example>
        [RegularExpression("^\\d{3}$")]
        [JsonProperty("option")]
        [JsonRequired]
        public string Option { get; set; }
    }
}