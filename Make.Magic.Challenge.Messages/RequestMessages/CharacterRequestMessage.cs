using Http.Request.Service.Messages;
using System.Runtime.Serialization;

namespace Make.Magic.Challenge.Messages.RequestMessages
{
    [DataContract]
    public class CharacterRequestMessage : RequestMessage
    {
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
