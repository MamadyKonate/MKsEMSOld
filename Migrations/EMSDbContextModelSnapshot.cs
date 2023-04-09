﻿// <auto-generated />
using System;
using MKsEMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MKsEMS.Migrations
{
    [DbContext(typeof(EMSDbContext))]
    partial class EMSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("MKsEMS.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AddressLine2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AddressLine3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("MKsEMS.Models.Credentials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("encPass")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("saltEnd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("saltStart")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("userEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("MKsEMS.Models.Leave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Allowance")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DenialReason")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LeaveStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LeaveTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Taken")
                        .HasColumnType("INTEGER");

                    b.Property<int>("userEmail")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Leaves");
                });

            modelBuilder.Entity("MKsEMS.Models.LeaveType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LeaveTypes");
                });

            modelBuilder.Entity("MKsEMS.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("DOB")
                        .HasColumnType("TEXT");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("LeaveEntitement")
                        .HasColumnType("REAL");

                    b.Property<double>("LeaveTaken")
                        .HasColumnType("REAL");

                    b.Property<int>("ManagerId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Salary")
                        .HasColumnType("REAL");

                    b.Property<double>("SickLeaveTaken")
                        .HasColumnType("REAL");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MKsEMS.Models.Employee", b =>
                {
                    b.HasBaseType("MKsEMS.Models.User");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("MKsEMS.Models.Manager", b =>
                {
                    b.HasBaseType("MKsEMS.Models.User");

                    b.HasDiscriminator().HasValue("Manager");
                });
#pragma warning restore 612, 618
        }
    }
}
