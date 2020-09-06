﻿// <auto-generated />
using System;
using Make.Magic.Challenge.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Make.Magic.Challenge.Infra.Migrations
{
    [DbContext(typeof(MakeMagicContext))]
    [Migration("20200906202922_Migration_02")]
    partial class Migration_02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Make.Magic.Challenge.Domain.Character.Models.Character", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("HouseId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Patronus")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Role")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("School")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("Make.Magic.Challenge.Domain.Character.Models.CharacterHouse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("HouseId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.ToTable("CharacterHouse");
                });

            modelBuilder.Entity("Make.Magic.Challenge.Domain.House.Models.House", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("Code")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ExternalId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("House");
                });

            modelBuilder.Entity("Make.Magic.Challenge.Domain.Character.Models.Character", b =>
                {
                    b.HasOne("Make.Magic.Challenge.Domain.Character.Models.CharacterHouse", "House")
                        .WithMany()
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Make.Magic.Challenge.Domain.Character.Models.CharacterHouse", b =>
                {
                    b.HasOne("Make.Magic.Challenge.Domain.House.Models.House", "House")
                        .WithMany()
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
