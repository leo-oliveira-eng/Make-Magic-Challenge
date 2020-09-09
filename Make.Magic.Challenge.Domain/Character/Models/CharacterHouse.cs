using BaseEntity.Domain.Entities;
using Messages.Core;
using System;
using HouseModel = Make.Magic.Challenge.Domain.House.Models.House;

namespace Make.Magic.Challenge.Domain.Character.Models
{
    public class CharacterHouse : Entity
    {
        #region Properties

        public long HouseId { get; private set; }

        public HouseModel House { get; private set; }

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, false)]
        CharacterHouse() { }

        internal CharacterHouse (HouseModel house) : base(Guid.NewGuid())
        {
            House = house;
            HouseId = house.Id;
        }

        #endregion

        #region Conversion Operators

        public static implicit operator CharacterHouse(Maybe<CharacterHouse> entity) => entity.Value;

        public static implicit operator CharacterHouse(Response<CharacterHouse> entity) => entity.Data;

        #endregion
    }
}
