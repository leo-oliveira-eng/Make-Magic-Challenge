using BaseEntity.Domain.Entities;
using System;
using Model = Make.Magic.Challenge.Domain.House.Models;

namespace Make.Magic.Challenge.Domain.Character.Models
{
    public class CharacterHouse : Entity
    {
        #region Properties

        public long HouseId { get; private set; }

        public Model.House House { get; private set; }

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, false)]
        CharacterHouse() { }

        internal CharacterHouse (Model.House house) : base(Guid.NewGuid())
        {
            House = house;
            HouseId = house.Id;
        }

        #endregion
    }
}
