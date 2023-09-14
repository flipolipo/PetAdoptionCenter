﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SimpleWebDal.Data;

#nullable disable

namespace SimpleWebDal.Migrations
{
    [DbContext(typeof(PetAdoptionCenterContext))]
    [Migration("20230914202940_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesRoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("integer");

                    b.HasKey("RolesRoleId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("SimpleWebDal.Models.CalendarModel.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("AcctivityDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ActivityId");

                    b.HasIndex("ActivityId")
                        .IsUnique();

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("SimpleWebDal.Models.CalendarModel.CalendarActivity", b =>
                {
                    b.Property<int>("CalendarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CalendarId"));

                    b.Property<DateTime>("DateWithTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("CalendarId");

                    b.HasIndex("CalendarId")
                        .IsUnique();

                    b.ToTable("Calendars");
                });

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
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.Credentials", b =>
                {
                    b.Property<int>("CredentialsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CredentialsId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CredentialsId");

                    b.HasIndex("CredentialsId")
                        .IsUnique();

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<int>("BasicInformationId")
                        .HasColumnType("integer");

                    b.Property<int>("CredentialsId")
                        .HasColumnType("integer");

                    b.Property<int>("UserCalendarCalendarId")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.HasIndex("BasicInformationId");

                    b.HasIndex("CredentialsId");

                    b.HasIndex("UserCalendarCalendarId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("SimpleWebDal.Models.WebUser.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleWebDal.Models.WebUser.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleWebDal.Models.CalendarModel.Activity", b =>
                {
                    b.HasOne("SimpleWebDal.Models.CalendarModel.CalendarActivity", null)
                        .WithMany("Activities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.User", b =>
                {
                    b.HasOne("SimpleWebDal.Models.WebUser.BasicInformation", "BasicInformation")
                        .WithMany()
                        .HasForeignKey("BasicInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleWebDal.Models.WebUser.Credentials", "Credentials")
                        .WithMany()
                        .HasForeignKey("CredentialsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleWebDal.Models.CalendarModel.CalendarActivity", "UserCalendar")
                        .WithMany()
                        .HasForeignKey("UserCalendarCalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BasicInformation");

                    b.Navigation("Credentials");

                    b.Navigation("UserCalendar");
                });

            modelBuilder.Entity("SimpleWebDal.Models.CalendarModel.CalendarActivity", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}