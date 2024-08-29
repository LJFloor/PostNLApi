using System.Runtime.Serialization;

namespace PostNLApi.Models.Response
{
    [DataContract]
    public class Label
    {
        /// <summary>
        /// The content of the label
        /// </summary>
        [DataMember(Name = "Content", IsRequired = true, EmitDefaultValue = false)]
        public byte[] Content { get; set; }

        /// <summary>
        /// Type of the label
        /// </summary>
        [DataMember(Name = "LabelType", IsRequired = true, EmitDefaultValue = false)]
        public string LabelType { get; set; }

        /// <summary>
        /// Type of the output
        /// </summary>
        [DataMember(Name = "OutputType", IsRequired = true, EmitDefaultValue = false)]
        public string OutputType { get; set; }
    }
}