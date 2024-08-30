using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PostNLApi.Models.Request
{
    /// <summary>
    /// Hazardous material for PostNL
    /// </summary>
    /// <remarks>https://developer.postnl.nl/docs/#/http/models/structures/hazardous-material</remarks>
    public class HazardousMaterial
    {
        /// <summary>
        /// Toxic substance code as stated in the ADR agreement
        /// </summary>
        [Required]
        public string ToxicSubstanceCode { get; set; }
        
        /// <summary>
        /// Array of additional toxic substance codes as stated in the ADR agreement
        /// </summary>
        public List<string> AdditionalToxicSubstanceCode { get; set; } = new List<string>();
        
        /// <summary>
        /// The amount of ADR points
        /// </summary>
        [JsonProperty("ADRPoints")]
        public string AdrPoints { get; set; }
        
        /// <summary>
        /// The code indicating for which category of tunnels passage is prohibited with these goods.
        /// </summary>
        public string TunnelCodes { get; set; }
        
        /// <summary>
        /// Code identifying the category of the packaging material.
        /// </summary>
        public string PackagingGroupCode { get; set; }
        
        /// <summary>
        /// Gross weight of the goods in grams.
        /// </summary>
        public int GrossWeight { get; set; }
        
        /// <summary>
        /// The UNDG number
        /// </summary>
        [JsonProperty("UNDGNumber")]
        public string UndgNumber { get; set; }
        
        /// <summary>
        /// The transport category code
        /// </summary>
        public string TransportCategoryCode { get; set; }
        
        /// <summary>
        /// The chemical technical description of the goods.
        /// </summary>
        public string ChemicalTechnicalDescription { get; set; }
    }
}