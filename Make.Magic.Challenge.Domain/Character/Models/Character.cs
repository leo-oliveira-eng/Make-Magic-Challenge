using BaseEntity.Domain.Entities;
using Messages.Core;
using Messages.Core.Extensions;
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

        #region Methods

        public static Response<Character> Create(string name, string role, string school, string house, string patronus)
        {
            var response = Response<Character>.Create();

            VerifyArguments(name, role, school, house, response);

            if (response.HasError)
                return response;

            return response.SetValue(new Character(name, role, school, house, patronus));
        }

        static void VerifyArguments(string name, string role, string school, string house, Response<Character> response)
        {
            if (string.IsNullOrEmpty(name))
                response.WithBusinessError(nameof(name), $"{nameof(name)} is invalid");

            if (string.IsNullOrEmpty(role))
                response.WithBusinessError(nameof(role), $"{nameof(role)} is invalid");

            if (string.IsNullOrEmpty(school))
                response.WithBusinessError(nameof(school), $"{nameof(school)} is invalid");

            if (string.IsNullOrEmpty(house))
                response.WithBusinessError(nameof(house), $"{nameof(house)} is invalid");
        }

        public Response Update(string name, string role, string school, string house, string patronus)
        {
            var response = Response<Character>.Create();

            VerifyArguments(name, role, school, house, response);

            if (response.HasError)
                return response;

            Name = name;
            Role = role;
            School = school;
            House = house;
            Patronus = patronus;

            return response;
        }

        #endregion
    }
}
