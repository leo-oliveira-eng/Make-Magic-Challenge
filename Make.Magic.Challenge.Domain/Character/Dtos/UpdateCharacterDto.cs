using System;

namespace Make.Magic.Challenge.Domain.Character.Dtos
{
    public class UpdateCharacterDto
    {
        public Guid Code { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string School { get; set; }

        public string House { get; set; }

        public string Patronus { get; set; }
    }
}
