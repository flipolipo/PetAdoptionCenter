﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SimpleWebDal.Data;

#nullable disable

namespace SimpleWebDal.Migrations
{
    [DbContext(typeof(PetAdoptionCenterContext))]
    partial class PetAdoptionCenterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AddressId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FlatNumber")
                        .HasColumnType("integer");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AddressId");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            AddressId = 1,
                            City = "Warsaw",
                            FlatNumber = 3,
                            HouseNumber = "3a",
                            PostalCode = "48-456",
                            Street = "Janasa"
                        },
                        new
                        {
                            AddressId = 2,
                            City = "Warsaw",
                            FlatNumber = 3,
                            HouseNumber = "3a",
                            PostalCode = "48-456",
                            Street = "Janasa"
                        },
                        new
                        {
                            AddressId = 3,
                            City = "Warsaw",
                            FlatNumber = 3,
                            HouseNumber = "3a",
                            PostalCode = "48-456",
                            Street = "Janasa"
                        },
                        new
                        {
                            AddressId = 4,
                            City = "Gdynia",
                            FlatNumber = 3,
                            HouseNumber = "3a",
                            PostalCode = "48-456",
                            Street = "Janasa"
                        });
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.BasicInformation", b =>
                {
                    b.Property<int>("BasicInformationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BasicInformationId"));

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BasicInformationId");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("BasicInformationId")
                        .IsUnique();

                    b.ToTable("BasicInformations");

                    b.HasData(
                        new
                        {
                            BasicInformationId = 1,
                            AddressId = 2,
                            Email = "filip@wp.pl",
                            Name = "Filip",
                            Phone = "345678904",
                            Surname = "Juroszek"
                        });
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.BasicInformation", b =>
                {
                    b.HasOne("SimpleWebDal.Models.WebUser.Address", "Address")
                        .WithOne()
                        .HasForeignKey("SimpleWebDal.Models.WebUser.BasicInformation", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });
#pragma warning restore 612, 618
        }
    }
}
