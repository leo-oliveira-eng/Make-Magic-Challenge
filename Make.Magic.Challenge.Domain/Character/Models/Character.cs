using BaseEntity.Domain.Entities;
using System;

namespace Make.Magic.Challenge.Domain.Character.Models
{
    public class Character : Entity
    {
        #region Properties

        public string Name { get; private set; }

        public string Role { get; private set; }

        public string School { get; private set; }

        public string House { get; private set; }

        public string Patronus { get; private set; }

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, true)]
        Character() { }

        Character(string name, string role, string school, string house, string patronus)
            : base(Guid.NewGuid())
        {
            Name = name;
            Role = role;
            School = school;
            House = house;
            Patronus = patronus;
        }



        #endregion
    }
}
