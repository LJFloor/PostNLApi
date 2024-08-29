using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PostNLApi.Models.Request
{
    public class Content
    {
        /// <summary>
        /// Description of goods
        /// </summary>
        [MaxLength(35)]
        public string Description { get; set; }

        /// <summary>
        /// Fill in the total of the item(s)
        /// </summary>
        [Range(1, 9999)]
        public int Quantity { get; set; }

        /// <summary>
        /// Net weight of goods in gram(gr)
        /// </summary>
        [JsonRequired]
        public int Weight { get; set; }

        /// <summary>
        /// Commercial (customs) value of goods. Fill in the value of the item(s).
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Specify every item with the standard HS commodity code (HS-code)
        /// </summary>
        /// <remarks>https://tarief.douane.nl/ite-tariff-public/#/home</remarks>
        // TODO: remove spaces in code
        [RegularExpression(@"^\d{6,10}$")]
        public string HsTariffNr { get; set; }

        /// <summary>
        /// Fill in the code of the country where the item was produced (ISO-code)
        /// </summary>
        /// <example>NL</example>
        /// <remarks>https://www.iso.org/home.html</remarks>
        // TODO: to uppercase in code
        [RegularExpression("^[A-Za-z]{2}$")]
        public string CountryOfOrigin { get; set; }
    }
}