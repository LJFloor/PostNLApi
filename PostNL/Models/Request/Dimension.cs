using System.ComponentModel.DataAnnotations;

namespace PostNLApi.Models.Request
{
    /// <summary>
    /// Parcle dimension for PostNL
    /// </summary>
    public class Dimension
    {
        /// <summary>
        /// Height in mm
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Length in mm
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Width in mm
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Volume of the shipment in centimeters (cm3).
        /// </summary>
        /// <remarks>Is automatically calculated based on the <see cref="Height"/>, <see cref="Length"/> and <see cref="Width"/></remarks>
        public int Volume => Height * Length * Width / 1000;

        /// <summary>
        /// Weight of the shipment in grams.
        /// </summary>
        /// <remarks>Approximate weight suffices</remarks>
        /// <example>2000</example>
        [Required]
        public int Weight { get; set; }
    }
}