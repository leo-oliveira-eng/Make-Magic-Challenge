﻿using BaseEntity.Domain.Entities;
using Messages.Core;
using Messages.Core.Extensions;
using System;

namespace Make.Magic.Challenge.Domain.House.Models
{
    public class House : Entity
    {
        #region Properties

        public string ExternalId { get; private set; }

        public string Name { get; private set; }

        #endregion

        #region Contructors

        [Obsolete(ConstructorObsoleteMessage, false)]
        public House() { }

        public House(string externalId, string name)
            : base(Guid.NewGuid())
        {
            ExternalId = externalId;
            Name = name;
        }

        #endregion

        #region Methods

        public static Response<House> Create(string name, string externalId)
        {
            var response = Response<House>.Create();

            if (string.IsNullOrEmpty(name))
                response.WithBusinessError(nameof(name), $"{nameof(name)} is invalid");

            if (string.IsNullOrEmpty(externalId))
                response.WithBusinessError(nameof(externalId), $"{nameof(externalId)} is invalid");

            if (response.HasError)
                return response;

            return response.SetValue(new House(externalId, name));
        }

        #endregion
    }
}
