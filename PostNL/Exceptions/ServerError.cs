using System;
using System.Linq;
using System.Runtime.Serialization;

namespace PostNLApi.Exceptions
{
    public class ServerError : ApiException
    {
        [DataMember(Name = "Errors", IsRequired = false, EmitDefaultValue = false)]
        public ErrorMessage[] Errors { get; set; } = Array.Empty<ErrorMessage>();

        [DataMember(Name = "Message", IsRequired = false, EmitDefaultValue = false)]
        public string message { get; set; }

        [DataMember(Name = "Fault", IsRequired = false, EmitDefaultValue = false)]
        public Fault fault { get; set; }

        // Yes, weird error handling. I tried merging the different models PostNL has for errors, but it's a mess.
        public override string Message => fault?.faultstring ?? message ?? string.Join(", ", Errors.Select(x => x.Description ?? x.ErrorMsg ?? ""));
    }

    public class ErrorMessage
    {
        [DataMember(Name = "Description", IsRequired = false, EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(Name = "ErrorMsg", IsRequired = false, EmitDefaultValue = false)]
        public string ErrorMsg { get; set; }
    }

    public class Fault
    {
        [DataMember(Name = "faultstring", IsRequired = false, EmitDefaultValue = false)]
        public string faultstring { get; set; }
    }
}