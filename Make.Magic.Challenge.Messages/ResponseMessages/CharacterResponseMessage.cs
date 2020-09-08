using Http.Request.Service.Messages;
using System;
using System.Runtime.Serialization;

namespace Make.Magic.Challenge.Messages.ResponseMessages
{
    [DataContract]
    public class CharacterResponseMessage : ResponseMessage
    {
        [DataMember]
        public Guid CharacterCode { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Role { get; set; }

        [DataMember]
        public string School { get; set; }

        [DataMember]
        public string House { get; set; }

        [DataMember]
        public string Patronus { get; set; }
    }
}
